using System;
using System.Windows.Forms;
using MSOPracticum;

namespace MSOPracticumUI
{
    public partial class Form1 : Form, IComponent
    {
        public string state { get; set; }
        public Presenter mediator { get; set; }

        public Form1()
        {
            mediator = Presenter.GetPresenter();
            mediator.UIComponent = this;
            InitializeComponent();
        }

        public void Receive(string message)
        {
            string[] splitMessage = message.Split(",");
            switch (splitMessage[0])
            {
                case "Input":
                    TxtInput.Text = splitMessage[1];
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = MSOPracticumUI.Properties.Resources.Sprite_0003;

            // picks state depending on selected output mode
            if (BtnMode1.Checked) state = "Parse,1,";
            else if (BtnMode2.Checked) state = "Parse,2,";
            else state = "Parse,3,";

            // then adds selected input mode to the state followed by the commands
            state += BtnFile.Text + ",";
            state += TxtInput.Text;

            mediator.Notify(this, state);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnFile_TextChanged(object sender, EventArgs e)
        {
            state = "Input," + BtnFile.Text.ToString();
            mediator.Notify(this, state);
        }
    }
}
