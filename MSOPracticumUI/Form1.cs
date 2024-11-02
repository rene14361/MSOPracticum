using System;
using System.Windows.Forms;

namespace MSOPracticumUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            CharBox1.Image = ImgLst.Images[1];
        }
    }
}
