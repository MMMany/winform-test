using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.Util;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var filter = @"Test file (*.txt)|*.txt";
            openFileDialog.Filter = filter;
            saveFileDialog.Filter = filter;
            
            logTextBox.ReadOnly = true;
            logTextBox.BackColor = System.Drawing.SystemColors.Window;
        }

        private void Debug(string message)
        {
            var action = new Action(() =>
            {
                if (logTextBox.TextLength == 0)
                {
                    logTextBox.AppendText(message);
                }
                else
                {
                    logTextBox.AppendText($"\n{message}");
                }
                logTextBox.ScrollToCaret();
            });
            
            if (logTextBox.InvokeRequired)
            {
                logTextBox.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        private WindowInfo CurrentWindow = null;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var res = MessageBox.Show(
                text: "Will be close?",
                caption: "Program close",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question,
                defaultButton: MessageBoxDefaultButton.Button2
            );
            if (res == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                // disposing
            }
            base.OnFormClosing(e);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var target = openFileDialog.FileName;
                Debug($"Opening file : {target}");
                var sr = new StreamReader(target);
                var data = sr.ReadToEnd();
                Debug($"Open complete (length: {data.Length})");
            }
            
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            var res = saveFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var target = saveFileDialog.FileName;
                Debug($"Saving to '{target}'");
                //var sw = new StreamWriter(target);
            }
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            if (findTextBox.TextLength != 0)
            {
                var window = WindowsUtil.FindWindow(findTextBox.Text);
                if (window != null)
                {
                    findResultLabel.Text = $"{window.Name} ({window.Bounds})";
                    Debug($"Window found : {window.Name} / {window.Bounds}");
                    CurrentWindow = window;
                }
                else
                {
                    findResultLabel.Text = "Not Found";
                    Debug($"Window not found : {findTextBox.Text}");
                    CurrentWindow = null;
                }
            }
        }

        private void moveWindowButton_Click(object sender, EventArgs e)
        {
            if (posXTestBox.TextLength == 0 || posYTextBox.TextLength == 0)
                return;

            if (CurrentWindow != null)
            {
                var x = int.Parse(posXTestBox.Text);
                var y = int.Parse(posYTextBox.Text);
                CurrentWindow.Move(x, y);
                findResultLabel.Text = $"{CurrentWindow.Name} ({CurrentWindow.Bounds})";
            }
        }

        private void resizeWindowButton_Click(object sender, EventArgs e)
        {
            if (widthTextBox.TextLength == 0 || heightTextBox.TextLength == 0)
                return;

            if (CurrentWindow != null)
            {
                var width = int.Parse(widthTextBox.Text);
                var height = int.Parse(heightTextBox.Text);
                CurrentWindow.Resize(width, height);
                findResultLabel.Text = $"{CurrentWindow.Name} ({CurrentWindow.Bounds})";
            }
        }

        private void numericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void findTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.findButton_Click(null, null);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void focusButton_Click(object sender, EventArgs e)
        {
            if (CurrentWindow != null)
            {
                CurrentWindow.SetForeground();
            }
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var image = new Image<Bgr, Byte>(dialog.FileName);
                    pictureBox1.Image = image.ToBitmap();
                }
            }
        }
    }
}
