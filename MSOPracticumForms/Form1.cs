using MSOPracticum;

namespace MSOPracticumUI
{
    public partial class Form1 : Form, IComponent
    {
        public string state { get; set; }
        private PictureBox[,] pictureBoxGrid = new PictureBox[5, 5];
        private PictureBox currentBox { get; set; }
        private Image currentImage { get; set; }
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

                case "Move":
                    int x, y = 0;
                    string[] numbers = splitMessage[1].Split(",");
                    int.TryParse(numbers[0], out x);
                    int.TryParse(numbers[1], out y);

                    // Calculates true modulo value, we have to do this because we need the modulo value for our grid system and in C# using % gives remainder not modulo
                    x = x % 5; if (x < 0) x += 5;
                    y = y % 5; if (y < 0) y += 5;

                    // Colours the previous box white and makes the box the player is standing on have the player's current sprite
                    currentBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0001;
                    currentBox = pictureBoxGrid[x, y];
                    currentBox.Image = currentImage;

                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxGrid[0, 0] = pictureBox1;
            pictureBoxGrid[1, 0] = pictureBox2;
            pictureBoxGrid[2, 0] = pictureBox3;
            pictureBoxGrid[3, 0] = pictureBox4;
            pictureBoxGrid[4, 0] = pictureBox5;
            pictureBoxGrid[0, 1] = pictureBox6;
            pictureBoxGrid[1, 1] = pictureBox7;
            pictureBoxGrid[2, 1] = pictureBox8;
            pictureBoxGrid[3, 1] = pictureBox9;
            pictureBoxGrid[4, 1] = pictureBox10;
            pictureBoxGrid[0, 2] = pictureBox11;
            pictureBoxGrid[1, 2] = pictureBox12;
            pictureBoxGrid[2, 2] = pictureBox13;
            pictureBoxGrid[3, 2] = pictureBox14;
            pictureBoxGrid[4, 2] = pictureBox15;
            pictureBoxGrid[0, 3] = pictureBox16;
            pictureBoxGrid[1, 3] = pictureBox17;
            pictureBoxGrid[2, 3] = pictureBox18;
            pictureBoxGrid[3, 3] = pictureBox19;
            pictureBoxGrid[4, 3] = pictureBox20;
            pictureBoxGrid[0, 4] = pictureBox21;
            pictureBoxGrid[1, 4] = pictureBox22;
            pictureBoxGrid[2, 4] = pictureBox23;
            pictureBoxGrid[3, 4] = pictureBox24;
            pictureBoxGrid[4, 4] = pictureBox25;

        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            TxtOutput.Text = String.Empty;
            foreach(PictureBox pictureBox in pictureBoxGrid) pictureBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0002;
            currentBox = pictureBox1;
            currentImage = MSOPracticumForms.Properties.Resources.Sprite_0003E;
            currentBox.Image = currentImage;

            // Picks state depending on selected output mode
            if (BtnMode1.Checked) state = "Parse|1|";
            else if (BtnMode2.Checked) state = "Parse|2|";
            else state = "Parse|3|";

            // Then adds selected input mode to the state followed by the commands
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
