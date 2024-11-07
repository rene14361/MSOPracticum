using System;
using System.Windows.Forms;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = MSOPracticumUI.Properties.Resources.Sprite_0003;

            if (BtnMode1.Checked) state = "Run,1,";
            else if (BtnMode2.Checked) state = "Run,2";
            else state = "Run,3,";
            state += TxtInput.Text;

            Notify();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
