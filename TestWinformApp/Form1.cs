using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Utils.RunOnSTA<string, string>((t1, t2) =>
            //{
            //    Logging.Logger.Debug(t1);
            //    Logging.Logger.Debug(t2);
            //}, "Hello", "World");
            

            var windowName = textBox1.Text.Trim();
            if (windowName.Length == 0) return;

            Logging.Logger.Debug("Do action");

            var thisWindow = new AppWindow(this);

            Task.Run(() =>
            {
                if (DoAction(windowName).Result)
                {
                    Logging.Logger.Debug("Completed action");

                    //var text = Utils.GetClipboardText();
                    var text = Utils.RunOnSTA(() => Clipboard.GetText(TextDataFormat.Text)).Result;

                    Logging.Logger.Debug($"Text: {text}");
                    label1.BeginInvoke(new Action(() => label1.Text = text));
                    thisWindow.SetForeground();
                }
                else
                {
                    Logging.Logger.Debug("Failed action");
                }
            });
        }

        private async Task<bool> DoAction(string windowName)
        {
            var window = await AppWindow.FindWIndowAsync(windowName);
            if (window == null) return false;

            // Change Focus
            window.SetForeground();
            await Task.Delay(100);

            // Select All
            WindowsInput.KeyboardDown(VirtualKey.LCONTROL);
            WindowsInput.KeyboardPress(VirtualKey.A);
            WindowsInput.KeyboardUp(VirtualKey.LCONTROL);

            // Copy
            WindowsInput.KeyboardDown(VirtualKey.LCONTROL);
            WindowsInput.KeyboardPress(VirtualKey.C);
            WindowsInput.KeyboardUp(VirtualKey.LCONTROL);

            return true;
        }
    }
}
