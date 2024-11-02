namespace MSOPracticumUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Codetxtbx = new System.Windows.Forms.TextBox();
            this.BtnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Codetxtbx
            // 
            this.Codetxtbx.Location = new System.Drawing.Point(201, 63);
            this.Codetxtbx.Multiline = true;
            this.Codetxtbx.Name = "Codetxtbx";
            this.Codetxtbx.Size = new System.Drawing.Size(295, 246);
            this.Codetxtbx.TabIndex = 1;
            // 
            // BtnRun
            // 
            this.BtnRun.Location = new System.Drawing.Point(233, 352);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 2;
            this.BtnRun.Text = "Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 511);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.Codetxtbx);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Codetxtbx;
        private System.Windows.Forms.Button BtnRun;
    }
}

