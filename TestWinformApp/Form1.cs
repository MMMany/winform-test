using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestWinformApp.Internal;
using WinUserApi;

namespace TestWinformApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private object _lock = new object();
        private bool _isRunning;

        protected override void OnLoad(EventArgs e)
        {
            Logging.Logger.Debug("OnLoad");
            StateLabel.Text = TestManager.Instance.State.ToString();
            ResultLabel.Text = TestResult.None.ToString();

            base.OnLoad(e);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            lock (_lock)
            {
                if (_isRunning)
                {
                    // Pause
                    TestManager.Instance.Pause();
                }
                else
                {
                    // Start
                    _isRunning = true;
                    ResultLabel.Text = TestResult.None.ToString();
                    StartButton.Text = @"Pause";
                    ResetButton.Enabled = false;

                    Task.Run(async () =>
                    {
                        var manager = TestManager.Instance;
                        try
                        {
                            manager.TestProgress += this.TestProgress;
                            manager.TestComplete += this.TestComplete;
                            await manager.Start();
                        }
                        catch (Exception ex)
                        {
                            if (!(ex is OperationCanceledException))
                            {
                                Logging.Logger.Error(ex, ex.Message);
                            }
                        }
                        finally
                        {
                            manager.TestProgress -= this.TestProgress;
                            manager.TestComplete -= this.TestComplete;
                        }
                    });
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            lock (_lock)
            {
                TestManager.Instance.Reset();
            }
        }

        private void TestProgress(object sender, string message)
        {
            this.BeginInvoke(new Action(() =>
            {
                StateLabel.Text = TestManager.Instance.State.ToString();
                AddLog(message);
            }));
        }

        private void TestComplete(object sender, TestResult result)
        {
            this.BeginInvoke(new Action(() =>
            {
                StateLabel.Text = TestManager.Instance.State.ToString();
                ResultLabel.Text = result.ToString();

                StartButton.Text = @"Start";
                ResetButton.Enabled = true;

                _isRunning = false;
            }));
        }

        private void AddLog(string message)
        {
            var time = DateTime.Now.ToString(@"yyMMdd_HH-mm-ss");
            message = $"[{time}] {message}";

            if (LogBox.Text.Length != 0)
            {
                message = $"\n{message}";
            }
            
            if (LogBox.InvokeRequired)
            {
                LogBox.BeginInvoke(new Action(() =>
                {
                    LogBox.AppendText(message);
                    LogBox.ScrollToCaret();
                }));
            }
            else
            {
                LogBox.AppendText(message);
                LogBox.ScrollToCaret();
            }
        }
    }
}
