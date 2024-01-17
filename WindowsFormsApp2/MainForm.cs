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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var filter = @"Test file (*.txt)|*.txt";
            openFileDialog.Filter = filter;
            saveFileDialog.Filter = filter;

            logTextBox.ReadOnly = true;
            logTextBox.BackColor = System.Drawing.SystemColors.Window;
        }

        //private void Debug(string message)
        //{
        //    var action = new Action(() =>
        //    {
        //        if (logTextBox.TextLength == 0)
        //        {
        //            logTextBox.AppendText(message);
        //        }
        //        else
        //        {
        //            logTextBox.AppendText($"\n{message}");
        //        }
        //        logTextBox.ScrollToCaret();
        //    });
            
        //    if (logTextBox.InvokeRequired)
        //    {
        //        logTextBox.BeginInvoke(action);
        //    }
        //    else
        //    {
        //        action();
        //    }
        //}

        private void Debug(params string[] messages)
        {
            void UpdateText(RichTextBox rtb, string text)
            {
                if (rtb.TextLength == 0)
                    rtb.AppendText(text);
                else
                    rtb.AppendText($"\n{text}");
                rtb.ScrollToCaret();
            }
            Action<RichTextBox, string> action = UpdateText;
            
            foreach (var message in messages)
            {
                if (logTextBox.InvokeRequired)
                    logTextBox.BeginInvoke(action, logTextBox, message);
                else
                    action(logTextBox, message);
            }
        }

        private AppWindow CurrentWindow = null;

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

        private void OpenMenuItem_Click(object sender, EventArgs e)
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

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            var res = saveFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var target = saveFileDialog.FileName;
                Debug($"Saving to '{target}'");
                //var sw = new StreamWriter(target);
            }
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (findTextBox.TextLength != 0)
            {
                var window = AppWindow.FindWindow(findTextBox.Text);
                findResultLabel.Text = $"{window.Name} ({window.Bounds})";
                Debug($"Window found : {window.Name} / {window.Bounds}");
                CurrentWindow = window;
            }
        }

        private void MoveWindowButton_Click(object sender, EventArgs e)
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

        private void ResizeWindowButton_Click(object sender, EventArgs e)
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

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FindButton_Click(null, null);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void FocusButton_Click(object sender, EventArgs e)
        {
            if (CurrentWindow != null)
            {
                CurrentWindow.SetForeground();
            }
        }

        private string sourcePath;
        private string targetPath;

        private void Load1Button_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = @"PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var image = new Image<Bgr, byte>(dialog.FileName);
                    pictureBox1.Image = image.ToBitmap();
                    sourcePath = dialog.FileName;
                }
            }
        }

        private void Load2Button_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = @"PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var image = new Image<Bgr, byte>(dialog.FileName);
                    pictureBox2.Image = image.ToBitmap();
                    targetPath = dialog.FileName;
                }
            }
        }

        private void ImageFindButton_Click(object sender, EventArgs e)
        {
            using (var source = new Image<Gray, byte>(sourcePath))
            using (var target = new Image<Gray, byte>(targetPath))
            using (var result = source.MatchTemplate(target, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                result.MinMax(
                    out double[] minValues, out double[] maxValues,
                    out Point[] minLocations, out Point[] maxLocations);

                if (maxValues.First() > 0.8)
                {
                    var match = new Rectangle(maxLocations[0], target.Size);
                    source.Draw(match, new Gray(255), 2);
                    pictureBox1.Image = source.ToBitmap();
                    Debug($"Found : {match}", $"Score : {maxValues.First()}");
                }
                else
                {
                    Debug("Not found");
                }
            }
        }
    }
}
