using MSOPracticum;

namespace MSOPracticumUI
{
    public partial class Form1 : Form, IComponent
    {
        public string state { get; set; }
        private Presenter mediator { get; set; }

        public Form1(Presenter presenter)
        {
            mediator = presenter;
            mediator.UIComponent = this;
            InitializeComponent();
        }

        public void Receive(string message)
        {
            string[] splitMessage = message.Split("|");
            switch (splitMessage[0])
            {
                case "Input":
                    TxtInput.Text = splitMessage[1];
                    break;

                case "Metrics" or "Commands":
                    string output = "";
                    if (!string.IsNullOrEmpty(TxtOutput.Text)) output += "\r\n";
                    output += splitMessage[1];
                    TxtOutput.Text += output;
                    break;

                case "Load":
                    TxtInput.Text = splitMessage[1];
                    BtnFile.Text = "Custom";
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            TxtOutput.Text = String.Empty;
            pictureBox1.Image = Properties.Resources.Sprite_0003;

            // picks state depending on selected output mode
            if (BtnMode1.Checked) state = "Parse|1|";
            else if (BtnMode2.Checked) state = "Parse|2|";
            else state = "Parse|3|";

            // then adds selected input mode to the state followed by the commands
            state += BtnFile.Text + "|";
            state += TxtInput.Text;

            mediator.Notify(this, state);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            TxtOutput.Text = String.Empty;
            mediator.Notify(this, "Load|" + BtnFile.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnFile_TextChanged(object sender, EventArgs e)
        {
            state = "Input|" + BtnFile.Text.ToString();
            mediator.Notify(this, state);
        }

    }
}
