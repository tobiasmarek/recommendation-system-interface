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
            this.RecBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.LoadFromLbl = new System.Windows.Forms.Label();
            this.PreProcLbl = new System.Windows.Forms.Label();
            this.EvalLbl = new System.Windows.Forms.Label();
            this.PostProcLbl = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MagGlassBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.OutputSignLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(209, 94);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 26);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "CSV file";
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
            // PreProcLbl
            // 
            this.PreProcLbl.AutoSize = true;
            this.PreProcLbl.Location = new System.Drawing.Point(24, 205);
            this.PreProcLbl.Name = "PreProcLbl";
            this.PreProcLbl.Size = new System.Drawing.Size(110, 18);
            this.PreProcLbl.TabIndex = 3;
            this.PreProcLbl.Text = "Pre-Processor:";
            // 
            // EvalLbl
            // 
            this.EvalLbl.AutoSize = true;
            this.EvalLbl.Location = new System.Drawing.Point(24, 257);
            this.EvalLbl.Name = "EvalLbl";
            this.EvalLbl.Size = new System.Drawing.Size(74, 18);
            this.EvalLbl.TabIndex = 4;
            this.EvalLbl.Text = "Evaluator:";
            // 
            // PostProcLbl
            // 
            this.PostProcLbl.AutoSize = true;
            this.PostProcLbl.Location = new System.Drawing.Point(24, 309);
            this.PostProcLbl.Name = "PostProcLbl";
            this.PostProcLbl.Size = new System.Drawing.Size(119, 18);
            this.PostProcLbl.TabIndex = 5;
            this.PostProcLbl.Text = "Post-Processor:";
            // 
            // comboBox3
            // 
            this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(209, 254);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(159, 26);
            this.comboBox3.TabIndex = 7;
            // 
            // comboBox4
            // 
            this.comboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(209, 306);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(159, 26);
            this.comboBox4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.MagGlassBtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.FileTextBox);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.RecBtn);
            this.panel1.Controls.Add(this.comboBox4);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.LoadFromLbl);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.PreProcLbl);
            this.panel1.Controls.Add(this.PostProcLbl);
            this.panel1.Controls.Add(this.EvalLbl);
            this.panel1.Location = new System.Drawing.Point(21, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 463);
            this.panel1.TabIndex = 9;
            // 
            // MagGlassBtn
            // 
            this.MagGlassBtn.FlatAppearance.BorderSize = 0;
            this.MagGlassBtn.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MagGlassBtn.Location = new System.Drawing.Point(346, 137);
            this.MagGlassBtn.Name = "MagGlassBtn";
            this.MagGlassBtn.Size = new System.Drawing.Size(20, 20);
            this.MagGlassBtn.TabIndex = 13;
            this.MagGlassBtn.Text = "🔎";
            this.MagGlassBtn.UseVisualStyleBackColor = true;
            this.MagGlassBtn.Click += new System.EventHandler(this.MagGlassBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(43, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "Enter file name or select";
            // 
            // FileTextBox
            // 
            this.FileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileTextBox.Font = new System.Drawing.Font("Roboto Medium", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileTextBox.Location = new System.Drawing.Point(209, 138);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(127, 15);
            this.FileTextBox.TabIndex = 11;
            this.FileTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FileTextBox_KeyPress);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(237, 385);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 10;
            this.button2.Text = "Demo 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(61, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 9;
            this.button1.Text = "Demo 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(209, 203);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(159, 26);
            this.comboBox2.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.OutputTextBox);
            this.panel2.Controls.Add(this.OutputSignLbl);
            this.panel2.Location = new System.Drawing.Point(431, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 463);
            this.panel2.TabIndex = 10;
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
            this.OutputTextBox.Size = new System.Drawing.Size(360, 360);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(918, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(48)))));
            this.MinimumSize = new System.Drawing.Size(936, 551);
            this.Name = "Form1";
            this.Text = "Recommendation System";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RecBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label LoadFromLbl;
        private System.Windows.Forms.Label PreProcLbl;
        private System.Windows.Forms.Label EvalLbl;
        private System.Windows.Forms.Label PostProcLbl;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private Panel panel1;
        private Panel panel2;
        private Label OutputSignLbl;
        private Button button2;
        private Button button1;
        private TextBox OutputTextBox;
        private ComboBox comboBox2;
        private Label label1;
        private TextBox FileTextBox;
        private Button MagGlassBtn;
    }
}