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
            this.TemplatePropertyLabel = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadParametersPnl = new System.Windows.Forms.Panel();
            this.MagGlassBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.SelectApproachLbl = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.approachComboBox = new System.Windows.Forms.ComboBox();
            this.approachParametersPnl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.waitingLbl = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.OutputSignLbl = new System.Windows.Forms.Label();
            this.waitingTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.loadParametersPnl.SuspendLayout();
            this.approachParametersPnl.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // TemplatePropertyLabel
            // 
            this.TemplatePropertyLabel.AutoSize = true;
            this.TemplatePropertyLabel.Enabled = false;
            this.TemplatePropertyLabel.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TemplatePropertyLabel.Location = new System.Drawing.Point(5, 11);
            this.TemplatePropertyLabel.Name = "TemplatePropertyLabel";
            this.TemplatePropertyLabel.Size = new System.Drawing.Size(54, 14);
            this.TemplatePropertyLabel.TabIndex = 3;
            this.TemplatePropertyLabel.Text = "template";
            this.TemplatePropertyLabel.Visible = false;
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.loadParametersPnl);
            this.panel1.Controls.Add(this.SelectApproachLbl);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.RecBtn);
            this.panel1.Controls.Add(this.loadFromComboBox);
            this.panel1.Controls.Add(this.LoadFromLbl);
            this.panel1.Controls.Add(this.approachComboBox);
            this.panel1.Controls.Add(this.approachParametersPnl);
            this.panel1.Location = new System.Drawing.Point(21, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 602);
            this.panel1.TabIndex = 9;
            // 
            // loadParametersPnl
            // 
            this.loadParametersPnl.BackColor = System.Drawing.Color.Lavender;
            this.loadParametersPnl.Controls.Add(this.MagGlassBtn);
            this.loadParametersPnl.Controls.Add(this.label1);
            this.loadParametersPnl.Controls.Add(this.FileTextBox);
            this.loadParametersPnl.Location = new System.Drawing.Point(37, 135);
            this.loadParametersPnl.Name = "loadParametersPnl";
            this.loadParametersPnl.Size = new System.Drawing.Size(335, 25);
            this.loadParametersPnl.TabIndex = 17;
            // 
            // MagGlassBtn
            // 
            this.MagGlassBtn.FlatAppearance.BorderSize = 0;
            this.MagGlassBtn.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MagGlassBtn.Location = new System.Drawing.Point(311, 2);
            this.MagGlassBtn.Name = "MagGlassBtn";
            this.MagGlassBtn.Size = new System.Drawing.Size(20, 20);
            this.MagGlassBtn.TabIndex = 13;
            this.MagGlassBtn.Text = "❏";
            this.MagGlassBtn.UseVisualStyleBackColor = true;
            this.MagGlassBtn.Click += new System.EventHandler(this.MagGlassBtn_Click);
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
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(237, 524);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 10;
            this.button2.Text = "Demo 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(61, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 9;
            this.button1.Text = "Demo 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // approachComboBox
            // 
            this.approachComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.approachComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.approachComboBox.FormattingEnabled = true;
            this.approachComboBox.Location = new System.Drawing.Point(209, 181);
            this.approachComboBox.Name = "approachComboBox";
            this.approachComboBox.Size = new System.Drawing.Size(159, 26);
            this.approachComboBox.TabIndex = 6;
            this.approachComboBox.SelectedValueChanged += new System.EventHandler(this.approachComboBox_SelectedValueChanged);
            // 
            // approachParametersPnl
            // 
            this.approachParametersPnl.BackColor = System.Drawing.Color.Lavender;
            this.approachParametersPnl.Controls.Add(this.TemplatePropertyLabel);
            this.approachParametersPnl.Controls.Add(this.templateComboBox);
            this.approachParametersPnl.Location = new System.Drawing.Point(37, 213);
            this.approachParametersPnl.Name = "approachParametersPnl";
            this.approachParametersPnl.Size = new System.Drawing.Size(335, 39);
            this.approachParametersPnl.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.waitingLbl);
            this.panel2.Controls.Add(this.OutputTextBox);
            this.panel2.Controls.Add(this.OutputSignLbl);
            this.panel2.Location = new System.Drawing.Point(431, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(602, 602);
            this.panel2.TabIndex = 10;
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
            // waitingTimer
            // 
            this.waitingTimer.Interval = 800;
            this.waitingTimer.Tick += new System.EventHandler(this.waitingTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1054, 643);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(48)))));
            this.MinimumSize = new System.Drawing.Size(1072, 690);
            this.Name = "Form1";
            this.Text = "Recommendation System";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.loadParametersPnl.ResumeLayout(false);
            this.loadParametersPnl.PerformLayout();
            this.approachParametersPnl.ResumeLayout(false);
            this.approachParametersPnl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RecBtn;
        private System.Windows.Forms.ComboBox loadFromComboBox;
        private System.Windows.Forms.Label LoadFromLbl;
        private System.Windows.Forms.Label TemplatePropertyLabel;
        private System.Windows.Forms.ComboBox templateComboBox;
        private Panel panel1;
        private Panel panel2;
        private Label OutputSignLbl;
        private Button button2;
        private Button button1;
        private TextBox OutputTextBox;
        private ComboBox approachComboBox;
        private Label label1;
        private TextBox FileTextBox;
        private Button MagGlassBtn;
        private Label SelectApproachLbl;
        private System.Windows.Forms.Timer waitingTimer;
        private Label waitingLbl;
        private Panel approachParametersPnl;
        private Panel loadParametersPnl;
    }
}