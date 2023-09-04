namespace WinFormsRecSys
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RecBtn = new System.Windows.Forms.Button();
            this.loadFromComboBox = new System.Windows.Forms.ComboBox();
            this.LoadFromLbl = new System.Windows.Forms.Label();
            this.templatePropertyLbl = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.leftSidePnl = new System.Windows.Forms.Panel();
            this.loadParametersPnl = new System.Windows.Forms.Panel();
            this.DirectoryBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.SelectApproachLbl = new System.Windows.Forms.Label();
            this.Demo2Btn = new System.Windows.Forms.Button();
            this.Demo1Btn = new System.Windows.Forms.Button();
            this.ApproachComboBox = new System.Windows.Forms.ComboBox();
            this.approachParametersPnl = new System.Windows.Forms.Panel();
            this.rightSidePnl = new System.Windows.Forms.Panel();
            this.userDefinitionTextBox = new System.Windows.Forms.TextBox();
            this.userPnl = new System.Windows.Forms.Panel();
            this.userTypeLbl = new System.Windows.Forms.Label();
            this.UserComboBox = new System.Windows.Forms.ComboBox();
            this.waitingLbl = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.OutputSignLbl = new System.Windows.Forms.Label();
            this.WaitingTimer = new System.Windows.Forms.Timer(this.components);
            this.leftSidePnl.SuspendLayout();
            this.loadParametersPnl.SuspendLayout();
            this.approachParametersPnl.SuspendLayout();
            this.rightSidePnl.SuspendLayout();
            this.userPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecBtn
            // 
            this.RecBtn.BackColor = System.Drawing.Color.Lavender;
            this.RecBtn.FlatAppearance.BorderSize = 5;
            this.RecBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RecBtn.Location = new System.Drawing.Point(23, 14);
            this.RecBtn.Name = "RecBtn";
            this.RecBtn.Size = new System.Drawing.Size(345, 52);
            this.RecBtn.TabIndex = 0;
            this.RecBtn.Text = "RECOMMEND";
            this.RecBtn.UseVisualStyleBackColor = false;
            this.RecBtn.Click += new System.EventHandler(this.RecBtn_Click);
            // 
            // loadFromComboBox
            // 
            this.loadFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loadFromComboBox.Enabled = false;
            this.loadFromComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFromComboBox.FormattingEnabled = true;
            this.loadFromComboBox.Items.AddRange(new object[] {
            "CSV file",
            "Database input"});
            this.loadFromComboBox.Location = new System.Drawing.Point(209, 94);
            this.loadFromComboBox.Name = "loadFromComboBox";
            this.loadFromComboBox.Size = new System.Drawing.Size(159, 26);
            this.loadFromComboBox.TabIndex = 1;
            // 
            // LoadFromLbl
            // 
            this.LoadFromLbl.AutoSize = true;
            this.LoadFromLbl.Location = new System.Drawing.Point(24, 96);
            this.LoadFromLbl.Name = "LoadFromLbl";
            this.LoadFromLbl.Size = new System.Drawing.Size(81, 18);
            this.LoadFromLbl.TabIndex = 2;
            this.LoadFromLbl.Text = "Load from:";
            // 
            // templatePropertyLbl
            // 
            this.templatePropertyLbl.AutoSize = true;
            this.templatePropertyLbl.Enabled = false;
            this.templatePropertyLbl.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.templatePropertyLbl.Location = new System.Drawing.Point(5, 11);
            this.templatePropertyLbl.Name = "templatePropertyLbl";
            this.templatePropertyLbl.Size = new System.Drawing.Size(54, 14);
            this.templatePropertyLbl.TabIndex = 3;
            this.templatePropertyLbl.Text = "template";
            this.templatePropertyLbl.Visible = false;
            // 
            // templateComboBox
            // 
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.Enabled = false;
            this.templateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.templateComboBox.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(171, 8);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(159, 22);
            this.templateComboBox.TabIndex = 7;
            this.templateComboBox.Visible = false;
            // 
            // leftSidePnl
            // 
            this.leftSidePnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftSidePnl.BackColor = System.Drawing.Color.AliceBlue;
            this.leftSidePnl.Controls.Add(this.loadParametersPnl);
            this.leftSidePnl.Controls.Add(this.SelectApproachLbl);
            this.leftSidePnl.Controls.Add(this.Demo2Btn);
            this.leftSidePnl.Controls.Add(this.Demo1Btn);
            this.leftSidePnl.Controls.Add(this.RecBtn);
            this.leftSidePnl.Controls.Add(this.loadFromComboBox);
            this.leftSidePnl.Controls.Add(this.LoadFromLbl);
            this.leftSidePnl.Controls.Add(this.ApproachComboBox);
            this.leftSidePnl.Controls.Add(this.approachParametersPnl);
            this.leftSidePnl.Location = new System.Drawing.Point(21, 21);
            this.leftSidePnl.Name = "leftSidePnl";
            this.leftSidePnl.Size = new System.Drawing.Size(387, 602);
            this.leftSidePnl.TabIndex = 9;
            // 
            // loadParametersPnl
            // 
            this.loadParametersPnl.BackColor = System.Drawing.Color.Lavender;
            this.loadParametersPnl.Controls.Add(this.DirectoryBtn);
            this.loadParametersPnl.Controls.Add(this.label1);
            this.loadParametersPnl.Controls.Add(this.FileTextBox);
            this.loadParametersPnl.Location = new System.Drawing.Point(37, 135);
            this.loadParametersPnl.Name = "loadParametersPnl";
            this.loadParametersPnl.Size = new System.Drawing.Size(335, 25);
            this.loadParametersPnl.TabIndex = 17;
            // 
            // DirectoryBtn
            // 
            this.DirectoryBtn.FlatAppearance.BorderSize = 0;
            this.DirectoryBtn.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DirectoryBtn.Location = new System.Drawing.Point(311, 2);
            this.DirectoryBtn.Name = "DirectoryBtn";
            this.DirectoryBtn.Size = new System.Drawing.Size(20, 20);
            this.DirectoryBtn.TabIndex = 13;
            this.DirectoryBtn.Text = "❏";
            this.DirectoryBtn.UseVisualStyleBackColor = true;
            this.DirectoryBtn.Click += new System.EventHandler(this.DirectoryBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "Enter file name or select";
            // 
            // FileTextBox
            // 
            this.FileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileTextBox.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileTextBox.Location = new System.Drawing.Point(171, 5);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(127, 15);
            this.FileTextBox.TabIndex = 11;
            this.FileTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FileTextBox_KeyPress);
            // 
            // SelectApproachLbl
            // 
            this.SelectApproachLbl.AutoSize = true;
            this.SelectApproachLbl.Location = new System.Drawing.Point(24, 184);
            this.SelectApproachLbl.Name = "SelectApproachLbl";
            this.SelectApproachLbl.Size = new System.Drawing.Size(116, 18);
            this.SelectApproachLbl.TabIndex = 14;
            this.SelectApproachLbl.Text = "Select approach";
            // 
            // Demo2Btn
            // 
            this.Demo2Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Demo2Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Demo2Btn.Location = new System.Drawing.Point(237, 524);
            this.Demo2Btn.Name = "Demo2Btn";
            this.Demo2Btn.Size = new System.Drawing.Size(94, 29);
            this.Demo2Btn.TabIndex = 10;
            this.Demo2Btn.Text = "Demo 2";
            this.Demo2Btn.UseVisualStyleBackColor = true;
            this.Demo2Btn.Click += new System.EventHandler(this.Demo2Btn_Click);
            // 
            // Demo1Btn
            // 
            this.Demo1Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Demo1Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Demo1Btn.Location = new System.Drawing.Point(61, 524);
            this.Demo1Btn.Name = "Demo1Btn";
            this.Demo1Btn.Size = new System.Drawing.Size(94, 29);
            this.Demo1Btn.TabIndex = 9;
            this.Demo1Btn.Text = "Demo 1";
            this.Demo1Btn.UseVisualStyleBackColor = true;
            this.Demo1Btn.Click += new System.EventHandler(this.Demo1Btn_Click);
            // 
            // ApproachComboBox
            // 
            this.ApproachComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ApproachComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApproachComboBox.FormattingEnabled = true;
            this.ApproachComboBox.Location = new System.Drawing.Point(209, 181);
            this.ApproachComboBox.Name = "ApproachComboBox";
            this.ApproachComboBox.Size = new System.Drawing.Size(159, 26);
            this.ApproachComboBox.TabIndex = 6;
            this.ApproachComboBox.SelectedValueChanged += new System.EventHandler(this.ApproachComboBox_SelectedValueChanged);
            // 
            // approachParametersPnl
            // 
            this.approachParametersPnl.BackColor = System.Drawing.Color.Lavender;
            this.approachParametersPnl.Controls.Add(this.templatePropertyLbl);
            this.approachParametersPnl.Controls.Add(this.templateComboBox);
            this.approachParametersPnl.Location = new System.Drawing.Point(37, 213);
            this.approachParametersPnl.Name = "approachParametersPnl";
            this.approachParametersPnl.Size = new System.Drawing.Size(335, 39);
            this.approachParametersPnl.TabIndex = 16;
            // 
            // rightSidePnl
            // 
            this.rightSidePnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightSidePnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rightSidePnl.Controls.Add(this.userDefinitionTextBox);
            this.rightSidePnl.Controls.Add(this.userPnl);
            this.rightSidePnl.Controls.Add(this.waitingLbl);
            this.rightSidePnl.Controls.Add(this.OutputTextBox);
            this.rightSidePnl.Controls.Add(this.OutputSignLbl);
            this.rightSidePnl.Location = new System.Drawing.Point(431, 21);
            this.rightSidePnl.Name = "rightSidePnl";
            this.rightSidePnl.Size = new System.Drawing.Size(602, 602);
            this.rightSidePnl.TabIndex = 10;
            // 
            // userDefinitionTextBox
            // 
            this.userDefinitionTextBox.AcceptsTab = true;
            this.userDefinitionTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.userDefinitionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userDefinitionTextBox.BackColor = System.Drawing.Color.Silver;
            this.userDefinitionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userDefinitionTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.userDefinitionTextBox.Location = new System.Drawing.Point(72, 487);
            this.userDefinitionTextBox.Multiline = true;
            this.userDefinitionTextBox.Name = "userDefinitionTextBox";
            this.userDefinitionTextBox.Size = new System.Drawing.Size(464, 50);
            this.userDefinitionTextBox.TabIndex = 4;
            this.userDefinitionTextBox.TabStop = false;
            // 
            // userPnl
            // 
            this.userPnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userPnl.BackColor = System.Drawing.Color.Silver;
            this.userPnl.Controls.Add(this.userTypeLbl);
            this.userPnl.Controls.Add(this.UserComboBox);
            this.userPnl.Location = new System.Drawing.Point(57, 450);
            this.userPnl.Name = "userPnl";
            this.userPnl.Size = new System.Drawing.Size(496, 103);
            this.userPnl.TabIndex = 5;
            // 
            // userTypeLbl
            // 
            this.userTypeLbl.AutoSize = true;
            this.userTypeLbl.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.userTypeLbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.userTypeLbl.Location = new System.Drawing.Point(15, 12);
            this.userTypeLbl.Name = "userTypeLbl";
            this.userTypeLbl.Size = new System.Drawing.Size(99, 14);
            this.userTypeLbl.TabIndex = 2;
            this.userTypeLbl.Text = "Pick a User Type:";
            // 
            // UserComboBox
            // 
            this.UserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserComboBox.BackColor = System.Drawing.Color.Silver;
            this.UserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UserComboBox.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserComboBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.UserComboBox.FormattingEnabled = true;
            this.UserComboBox.Location = new System.Drawing.Point(333, 9);
            this.UserComboBox.Name = "UserComboBox";
            this.UserComboBox.Size = new System.Drawing.Size(151, 22);
            this.UserComboBox.TabIndex = 1;
            this.UserComboBox.SelectedValueChanged += new System.EventHandler(this.UserComboBox_SelectedValueChanged);
            // 
            // waitingLbl
            // 
            this.waitingLbl.AutoSize = true;
            this.waitingLbl.Location = new System.Drawing.Point(57, 19);
            this.waitingLbl.Name = "waitingLbl";
            this.waitingLbl.Size = new System.Drawing.Size(0, 18);
            this.waitingLbl.TabIndex = 3;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.AcceptsTab = true;
            this.OutputTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.BackColor = System.Drawing.Color.LightGray;
            this.OutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OutputTextBox.Location = new System.Drawing.Point(57, 54);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(496, 499);
            this.OutputTextBox.TabIndex = 2;
            this.OutputTextBox.TabStop = false;
            // 
            // OutputSignLbl
            // 
            this.OutputSignLbl.AutoSize = true;
            this.OutputSignLbl.Font = new System.Drawing.Font("Roboto Medium", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OutputSignLbl.Location = new System.Drawing.Point(22, 14);
            this.OutputSignLbl.Name = "OutputSignLbl";
            this.OutputSignLbl.Size = new System.Drawing.Size(26, 30);
            this.OutputSignLbl.TabIndex = 0;
            this.OutputSignLbl.Text = ">";
            // 
            // WaitingTimer
            // 
            this.WaitingTimer.Interval = 800;
            this.WaitingTimer.Tick += new System.EventHandler(this.WaitingTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1054, 643);
            this.Controls.Add(this.rightSidePnl);
            this.Controls.Add(this.leftSidePnl);
            this.Font = new System.Drawing.Font("Roboto Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(48)))));
            this.MinimumSize = new System.Drawing.Size(1072, 690);
            this.Name = "Form1";
            this.Text = "Recommendation System";
            this.leftSidePnl.ResumeLayout(false);
            this.leftSidePnl.PerformLayout();
            this.loadParametersPnl.ResumeLayout(false);
            this.loadParametersPnl.PerformLayout();
            this.approachParametersPnl.ResumeLayout(false);
            this.approachParametersPnl.PerformLayout();
            this.rightSidePnl.ResumeLayout(false);
            this.rightSidePnl.PerformLayout();
            this.userPnl.ResumeLayout(false);
            this.userPnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RecBtn;
        private System.Windows.Forms.ComboBox loadFromComboBox;
        private System.Windows.Forms.Label LoadFromLbl;
        private System.Windows.Forms.Label templatePropertyLbl;
        private System.Windows.Forms.ComboBox templateComboBox;
        private Panel leftSidePnl;
        private Panel rightSidePnl;
        private Label OutputSignLbl;
        private Button Demo2Btn;
        private Button Demo1Btn;
        private TextBox OutputTextBox;
        private ComboBox ApproachComboBox;
        private Label label1;
        private TextBox FileTextBox;
        private Button DirectoryBtn;
        private Label SelectApproachLbl;
        private System.Windows.Forms.Timer WaitingTimer;
        private Label waitingLbl;
        private Panel approachParametersPnl;
        private Panel loadParametersPnl;
        private TextBox userDefinitionTextBox;
        private Panel userPnl;
        private ComboBox UserComboBox;
        private Label userTypeLbl;
    }
}