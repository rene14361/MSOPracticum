using MSOPracticum;
using System.Windows.Forms;

namespace MSOPracticumUI
{
    public partial class ApplicationForm : Form, IComponent
    {
        private bool drawPathReady = false;
        private bool exerciseReady = false; // readiness to execute the exercise, true when exercise grid was loaded correctly
        private string[] exerciseValues { get; set; } // used to reset the grid to the exercise's grid
        MSOPracticum.Point exerciseGoal { get; set; } // used to reset the grid to the exercise's grid
        private List<PointF> path = new List<PointF>();
        private PictureBox[,] pictureBoxGrid = new PictureBox[5, 5];
        private PictureBox currentBox { get; set; }
        private Image currentImage { get; set; }
        private Presenter mediator { get; set; }

        public ApplicationForm(Presenter presenter)
        {
            mediator = presenter;
            mediator.UIComponent = this;
            InitializeComponent();
            Paint += new PaintEventHandler(DrawPath_Paint);
        }

        public void Receive(string message)
        {
            string[] splitMessage = message.Split("|");
            switch (splitMessage[0])
            {
                // Puts text from example into the input field
                case "Example":
                    TxtInput.Text = splitMessage[1];
                    break;
                
                // Puts output text into the output field and makes sure to start a new line if the output field already contains text
                case "Metrics" or "Commands":
                    string output = "";
                    if (!string.IsNullOrEmpty(TxtOutput.Text)) output += "\r\n";
                    if (splitMessage[1] == "Outside" || splitMessage[1] == "Blocked")
                    {
                        output += splitMessage[2];
                        TxtOutput.Text += output;
                        currentBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0006;
                        return;
                    }
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
                    
                    // Makes the previous box white and makes the box the player is standing on have the player's current sprite
                    // If in exercise mode, makes sure that the goal is always displayed unless the player is standing on it
                    currentBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0001;
                    if (exerciseReady) pictureBoxGrid[exerciseGoal.X, exerciseGoal.Y].Image = MSOPracticumForms.Properties.Resources.Sprite_0004;
                    currentBox = pictureBoxGrid[x, y];
                    currentBox.Image = currentImage;

                    AddToPath();
                    drawPathReady = true;

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
                    exerciseValues = splitMessage[1].Split(",");
                    string[] goalValues = splitMessage[2].Split(",");

                    exerciseGoal = new MSOPracticum.Point(int.Parse(goalValues[0]), int.Parse(goalValues[1]));

                    MakeExerciseGrid();

                    // Changes the dropdown button selection to "Custom" and indicates that the program is ready to run an exercise
                    BtnFile.Text = "Custom";
                    exerciseReady = true;
                    break;
            }
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
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
            Reset();

            // If not in exclusively metrics mode, set current box and image values for later use
            if (!BtnMode2.Checked)
            {
                currentBox = pictureBox1;
                currentImage = MSOPracticumForms.Properties.Resources.Sprite_0003E;
                AddToPath();
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
            Invalidate();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            TxtOutput.Text = String.Empty;
            mediator.Notify(this, "Load|" + BtnFile.Text);
        }

        private void BtnFile_TextChanged(object sender, EventArgs e)
        {
            string message = "Example|" + BtnFile.Text.ToString();
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
            TxtOutput.Text = String.Empty;
            // Resets everything related to path and forces a redraw
            drawPathReady = false;
            path.Clear();
            Invalidate();

            // Resets all sprites and makes the top left block display the player with red background, unless ready for exercise, in which case resets to the exercise grid
            if (!exerciseReady) { foreach (PictureBox pictureBox in pictureBoxGrid) pictureBox.Image = MSOPracticumForms.Properties.Resources.Sprite_0002; pictureBox1.Image = MSOPracticumForms.Properties.Resources.Sprite_0005E; }
            else MakeExerciseGrid();
        }

        private void MakeExerciseGrid()
        {
            int i = 0; int j = 0;

            // Colours "True" boxes to white, makes others red
            foreach (string value in exerciseValues)
            {
                if (value == "True") pictureBoxGrid[i, j].Image = MSOPracticumForms.Properties.Resources.Sprite_0001;
                else pictureBoxGrid[i, j].Image = MSOPracticumForms.Properties.Resources.Sprite_0002;

                if (i == 4 && j == 4) break;
                if (i < 4) i++;
                else { i = 0; j++; }
            }

            // Sets the sprites for the target and player boxes
            pictureBox1.Image = MSOPracticumForms.Properties.Resources.Sprite_0003E;
            pictureBoxGrid[exerciseGoal.X, exerciseGoal.Y].Image = MSOPracticumForms.Properties.Resources.Sprite_0004;
        }

        private void AddToPath()
        {
            // Adds the current box's center to the list of points that need to be drawn
            System.Drawing.Point center = new System.Drawing.Point(currentBox.Location.X + currentBox.Width / 2, currentBox.Location.Y + currentBox.Height / 2);
            path.Add(new PointF(center.X, center.Y));
        }

        private void DrawPath_Paint(object sender, PaintEventArgs e)
        {
            // If the app is not ready to draw the path, return
            if (!drawPathReady) return;

            Pen pen = new Pen(Color.Blue, 3);
            for (int i = 1; i < path.Count; i++)
            {
                e.Graphics.DrawLine(pen, path[i-1], path[i]);
            }
        }
    }
}
