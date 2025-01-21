using MSOPracticum;
using System.Runtime.CompilerServices;

namespace MSOPracticumUI
{
    public partial class Form1 : Form, IComponent
    {
        private bool exerciseMode = false;
        private bool exerciseReady = false; // readiness to execute the exercise, true when exercise grid was loaded correctly
        private PictureBox[,] pictureBoxGrid = new PictureBox[5, 5];
        private bool[,] exerciseGrid = new bool[5, 5];
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
                // Puts text from example into the input field
                case "Input":
                    TxtInput.Text = splitMessage[1];
                    break;
                
                // Puts output text into the output field and makes sure to start a new line if the output field already contains text
                case "Metrics" or "Commands":
                    string output = "";
                    if (!string.IsNullOrEmpty(TxtOutput.Text)) output += "\r\n";
                    output += splitMessage[1];
                    TxtOutput.Text += output;
                    break;

                // Loads text from a file into the input field and changes the dropdown button selection to "Custom"
                case "Load":
                    TxtInput.Text = splitMessage[1];
                    BtnFile.Text = "Custom";
                    break;

                // Updates the grid with the character's current and visited positions
                case "Move":
                    int x, y;
                    string[] numbers = splitMessage[1].Split(",");
                    int.TryParse(numbers[0], out x);
                    int.TryParse(numbers[1], out y);

                    // Colours the previous box white and makes the box the player is standing on have the player's current sprite
                    currentBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0001;
                    currentBox = pictureBoxGrid[x, y];
                    currentBox.Image = currentImage;

                    break;

                // Changes the current character sprite based on the direction the character is facing
                case "Turn":
                    currentImage = splitMessage[1] switch
                    {
                        "north" => MSOPracticumForms.Properties.Resources.Sprite_0003N,
                        "south" => MSOPracticumForms.Properties.Resources.Sprite_0003S,
                        "west" => MSOPracticumForms.Properties.Resources.Sprite_0003W,
                        _ => MSOPracticumForms.Properties.Resources.Sprite_0003E,
                    };
                    currentBox.Image = currentImage;
                    break;

                // Loads the contents of an exercise into the grid
                case "Exercise":
                    string[] boolValues = splitMessage[1].Split(",");
                    string[] goalValues = splitMessage[2].Split(",");

                    MSOPracticum.Point goal = new MSOPracticum.Point(int.Parse(goalValues[0]), int.Parse(goalValues[1]));

                    int i = 0; int j = 0;

                    // Colours "True" boxes to white, keeps others red.
                    foreach (string value in boolValues)
                    {
                        if (value == "True") pictureBoxGrid[i, j].Image = MSOPracticumForms.Properties.Resources.Sprite_0001;
                        if (i == 4 && j == 4) break;
                        if (i < 4) i++;
                        else { i = 0; j++; }
                    }

                    pictureBoxGrid[goal.X, goal.Y].Image = MSOPracticumForms.Properties.Resources.Sprite_0004;

                    exerciseReady = true;
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
            Reset();

            // If not in exclusively metrics mode, display the character at the top left and set current box and image values for later use
            if (!BtnMode2.Checked)
            {
                currentBox = pictureBox1;
                currentImage = MSOPracticumForms.Properties.Resources.Sprite_0003E;
                currentBox.Image = currentImage;
            }

            // Creates string for mediator depending on selected output mode and whether the user is trying to attempt an exercise
            string message = (!exerciseReady) ? "Parse|" : "Attempt|";
            if (BtnMode1.Checked) message += "1|";
            else if (BtnMode2.Checked) message += "2|";
            else message += "3|";

            // Then adds selected input mode to the state followed by the commands
            message += BtnFile.Text + "|";
            message += TxtInput.Text;

            mediator.Notify(this, message);
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
            string message = "Input|" + BtnFile.Text.ToString();
            mediator.Notify(this, message);
        }

        private void BtnExercise_Click(object sender, EventArgs e)
        {
            exerciseReady = false;
            Reset();
            string message = "Exercise|" + BtnFile.Text.ToString();
            mediator.Notify(this, message);
        }

        private void Reset()
        {
            // Resets all sprites unless ready for exercise
            if (!exerciseReady) foreach (PictureBox pictureBox in pictureBoxGrid) pictureBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0002;
        }
    }
}
