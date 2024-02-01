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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                this.button1_Click(null, null);
            }
        }

        private Stack<Dummy> _stack = new Stack<Dummy>();
        private CancellationTokenSource _cts;

        private void button1_Click(object sender, EventArgs e)
        {
            //var windowName = textBox1.Text.Trim();
            //if (windowName.Length == 0) return;
            //var thisWindow = new AppWindow(this);

            Logging.Logger.Debug("Do action");
            var items = new[]
            {
                new Dummy("TC-1", 5),
                new Dummy("TC-2", 7),
                new Dummy("TC-3", 10)
            };
            foreach (var it in items.Reverse())
            {
                _stack.Push(it);
            }

            _cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    if (DoDummyAsync().Result)
                    {
                        Logging.Logger.Debug("Completed action");
                    }
                    else
                    {
                        Logging.Logger.Debug("Failed action");
                    }
                }
                catch (OperationCanceledException)
                {
                    Logging.Logger.Debug($"Main Loop: Cancelled");
                }
                catch (AggregateException aggEx)
                {
                    foreach (var ex in aggEx.InnerExceptions)
                    {
                        if (!(ex is OperationCanceledException))
                            Logging.Logger.Error($"Error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Logging.Logger.Error($"Error: {ex.Message}");
                }
                finally
                {
                    _cts?.Dispose();
                    _cts = null;
                }
            }, _cts.Token);
        }

        private async Task<bool> DoAsync(string windowName)
        {
            var window = await AppWindow.FindWIndowAsync(windowName);
            if (window == null) return false;
            Logging.Logger.Debug($"Handle: {window.Handle}");
            Logging.Logger.Debug($"Name: {window.Name}");
            Logging.Logger.Debug($"Bounds: {window.Bounds}");

            while (_stack.Count != 0)
            {
                var it = _stack.Pop();
                await it.RunAsync(_cts.Token);
            }

            return true;
        }

        private async Task<bool> DoDummyAsync()
        {
            while (_stack.Count != 0)
            {
                try
                {
                    var it = _stack.Pop();
                    await it.RunAsync(_cts.Token);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }
    }
}
