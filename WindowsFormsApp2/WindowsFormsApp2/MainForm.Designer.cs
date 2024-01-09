
namespace WindowsFormsApp2
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.findResultLabel = new System.Windows.Forms.Label();
            this.moveWindowButton = new System.Windows.Forms.Button();
            this.resizeWindowButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.posXTestBox = new System.Windows.Forms.TextBox();
            this.posYTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.focusButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(426, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.settingsMenuItem,
            this.toolStripSeparator1,
            this.closeMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(199, 22);
            this.openMenuItem.Text = "Open...";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveMenuItem.Text = "Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveAsMenuItem.Text = "Save As...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.settingsMenuItem.Size = new System.Drawing.Size(199, 22);
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(199, 22);
            this.closeMenuItem.Text = "Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Save As";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Target";
            // 
            // findTextBox
            // 
            this.findTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findTextBox.Location = new System.Drawing.Point(62, 32);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(271, 21);
            this.findTextBox.TabIndex = 101;
            this.findTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.findTextBox_KeyDown);
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.Location = new System.Drawing.Point(339, 31);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 102;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Window :";
            // 
            // findResultLabel
            // 
            this.findResultLabel.AutoSize = true;
            this.findResultLabel.Location = new System.Drawing.Point(78, 63);
            this.findResultLabel.Name = "findResultLabel";
            this.findResultLabel.Size = new System.Drawing.Size(63, 12);
            this.findResultLabel.TabIndex = 6;
            this.findResultLabel.Text = "Not Found";
            // 
            // moveWindowButton
            // 
            this.moveWindowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveWindowButton.Location = new System.Drawing.Point(302, 86);
            this.moveWindowButton.Name = "moveWindowButton";
            this.moveWindowButton.Size = new System.Drawing.Size(75, 23);
            this.moveWindowButton.TabIndex = 107;
            this.moveWindowButton.Text = "Move";
            this.moveWindowButton.UseVisualStyleBackColor = true;
            this.moveWindowButton.Click += new System.EventHandler(this.moveWindowButton_Click);
            // 
            // resizeWindowButton
            // 
            this.resizeWindowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resizeWindowButton.Location = new System.Drawing.Point(302, 115);
            this.resizeWindowButton.Name = "resizeWindowButton";
            this.resizeWindowButton.Size = new System.Drawing.Size(75, 23);
            this.resizeWindowButton.TabIndex = 108;
            this.resizeWindowButton.Text = "Resize";
            this.resizeWindowButton.UseVisualStyleBackColor = true;
            this.resizeWindowButton.Click += new System.EventHandler(this.resizeWindowButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pos X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pos Y";
            // 
            // posXTestBox
            // 
            this.posXTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.posXTestBox.Location = new System.Drawing.Point(62, 86);
            this.posXTestBox.Name = "posXTestBox";
            this.posXTestBox.ShortcutsEnabled = false;
            this.posXTestBox.Size = new System.Drawing.Size(190, 21);
            this.posXTestBox.TabIndex = 103;
            this.posXTestBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTextBox_KeyPress);
            // 
            // posYTextBox
            // 
            this.posYTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.posYTextBox.Location = new System.Drawing.Point(62, 114);
            this.posYTextBox.Name = "posYTextBox";
            this.posYTextBox.ShortcutsEnabled = false;
            this.posYTextBox.Size = new System.Drawing.Size(190, 21);
            this.posYTextBox.TabIndex = 104;
            this.posYTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Width";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widthTextBox.Location = new System.Drawing.Point(62, 143);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.ShortcutsEnabled = false;
            this.widthTextBox.Size = new System.Drawing.Size(190, 21);
            this.widthTextBox.TabIndex = 105;
            this.widthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTextBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Height";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightTextBox.Location = new System.Drawing.Point(62, 171);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.ShortcutsEnabled = false;
            this.heightTextBox.Size = new System.Drawing.Size(190, 21);
            this.heightTextBox.TabIndex = 106;
            this.heightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTextBox_KeyPress);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(12, 198);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(402, 189);
            this.logTextBox.TabIndex = 13;
            this.logTextBox.Text = "";
            // 
            // focusButton
            // 
            this.focusButton.Location = new System.Drawing.Point(302, 144);
            this.focusButton.Name = "focusButton";
            this.focusButton.Size = new System.Drawing.Size(75, 23);
            this.focusButton.TabIndex = 109;
            this.focusButton.Text = "Focus";
            this.focusButton.UseVisualStyleBackColor = true;
            this.focusButton.Click += new System.EventHandler(this.focusButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 399);
            this.Controls.Add(this.focusButton);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.posYTextBox);
            this.Controls.Add(this.posXTestBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resizeWindowButton);
            this.Controls.Add(this.moveWindowButton);
            this.Controls.Add(this.findResultLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.findTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label findResultLabel;
        private System.Windows.Forms.Button moveWindowButton;
        private System.Windows.Forms.Button resizeWindowButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox posXTestBox;
        private System.Windows.Forms.TextBox posYTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button focusButton;
    }
}

