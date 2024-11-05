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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtnRun = new System.Windows.Forms.Button();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.ImgLst = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRun
            // 
            this.BtnRun.Location = new System.Drawing.Point(257, 367);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 0;
            this.BtnRun.Text = "Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // performanceCounter1
            // 
            this.performanceCounter1.CategoryName = "Processor";
            this.performanceCounter1.CounterName = "% Processor Time";
            this.performanceCounter1.InstanceName = "_Total";
            // 
            // ImgLst
            // 
            this.ImgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgLst.ImageStream")));
            this.ImgLst.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgLst.Images.SetKeyName(0, "3a6469083e633a02f132c2dea8538e8f.jpg");
            this.ImgLst.Images.SetKeyName(1, "8b4e6aff-b17f-4c36-97ba-26e277bc6832_c1b3d0fd-8fdd-410c-abe1-6a05b7f5f35e.jpg");
            this.ImgLst.Images.SetKeyName(2, "1672935079261.jpeg");
            this.ImgLst.Images.SetKeyName(3, "19136fe872bb50b8103f88f66c8f9184_w200.gif");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.BtnRun);
            this.Name = "Form1";
            this.Text = "Learn To Program!";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button BtnRun;
        public System.Diagnostics.PerformanceCounter performanceCounter1;
        public System.Windows.Forms.ImageList ImgLst;
    }
}

