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
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RecBtn
            // 
            this.RecBtn.Location = new System.Drawing.Point(30, 28);
            this.RecBtn.Name = "RecBtn";
            this.RecBtn.Size = new System.Drawing.Size(307, 58);
            this.RecBtn.TabIndex = 0;
            this.RecBtn.Text = "RECOMMEND";
            this.RecBtn.UseVisualStyleBackColor = true;
            this.RecBtn.Click += new System.EventHandler(this.RecBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(195, 102);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 28);
            this.comboBox1.TabIndex = 1;
            // 
            // LoadFromLbl
            // 
            this.LoadFromLbl.AutoSize = true;
            this.LoadFromLbl.Location = new System.Drawing.Point(30, 105);
            this.LoadFromLbl.Name = "LoadFromLbl";
            this.LoadFromLbl.Size = new System.Drawing.Size(81, 20);
            this.LoadFromLbl.TabIndex = 2;
            this.LoadFromLbl.Text = "Load from:";
            // 
            // PreProcLbl
            // 
            this.PreProcLbl.AutoSize = true;
            this.PreProcLbl.Location = new System.Drawing.Point(30, 147);
            this.PreProcLbl.Name = "PreProcLbl";
            this.PreProcLbl.Size = new System.Drawing.Size(102, 20);
            this.PreProcLbl.TabIndex = 3;
            this.PreProcLbl.Text = "Pre-Processor:";
            // 
            // EvalLbl
            // 
            this.EvalLbl.AutoSize = true;
            this.EvalLbl.Location = new System.Drawing.Point(30, 189);
            this.EvalLbl.Name = "EvalLbl";
            this.EvalLbl.Size = new System.Drawing.Size(74, 20);
            this.EvalLbl.TabIndex = 4;
            this.EvalLbl.Text = "Evaluator:";
            // 
            // PostProcLbl
            // 
            this.PostProcLbl.AutoSize = true;
            this.PostProcLbl.Location = new System.Drawing.Point(30, 232);
            this.PostProcLbl.Name = "PostProcLbl";
            this.PostProcLbl.Size = new System.Drawing.Size(108, 20);
            this.PostProcLbl.TabIndex = 5;
            this.PostProcLbl.Text = "Post-Processor:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(195, 144);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(142, 28);
            this.comboBox2.TabIndex = 6;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(195, 186);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(142, 28);
            this.comboBox3.TabIndex = 7;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(195, 229);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(142, 28);
            this.comboBox4.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.PostProcLbl);
            this.Controls.Add(this.EvalLbl);
            this.Controls.Add(this.PreProcLbl);
            this.Controls.Add(this.LoadFromLbl);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.RecBtn);
            this.Name = "Form1";
            this.Text = "Recommendation System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RecBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label LoadFromLbl;
        private System.Windows.Forms.Label PreProcLbl;
        private System.Windows.Forms.Label EvalLbl;
        private System.Windows.Forms.Label PostProcLbl;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
    }
}