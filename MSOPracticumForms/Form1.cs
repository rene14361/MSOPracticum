using System;
using System.Windows.Forms;
using MSOPracticum;
using MSOPracticumPresenter;

namespace MSOPracticumUI
{
    public partial class Form1 : Form, ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        public string state { get; set; }

        public Form1(IObserver observer)
        {
            Attach(observer);
            InitializeComponent();
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers) observer.Update(this);
        }

        public void Attach(IObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
        }

        public void ExecuteResponse(string response)
        {
            switch (response)
            {
                // create parser, pass the state, and notify its observer that it's ready to run
                case "Parse":
                    Parser parser = new Parser();
                    parser.state = state;
                    parser.Notify();
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
            if (BtnMode1.Checked) state = "Run,1,";
            else if (BtnMode2.Checked) state = "Run,2,";
            else state = "Run,3,";

            // then adds selected input mode to the state followed by the commands
            // state += BtnFile.Text + ",";
            state += TxtInput.Text;

            Notify();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnFile_TextChanged(object sender, EventArgs e)
        {
            switch (BtnFile.SelectedItem.ToString())
            {
                case "Basic":
                    TxtInput.Text = Example.ReturnExample(1);
                    break;

                case "Advanced":
                    TxtInput.Text = Example.ReturnExample(2);
                    break;

                case "Expert":
                    TxtInput.Text = Example.ReturnExample(3);
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
