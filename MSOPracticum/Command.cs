using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOPracticum
{
    public class Command
    {
        List<string> commandList = new List<string>();
        List<int> commandNestingLevels = new List<int>();
        public Command (List<string> commandList, List<int> commandNestingLevels)
        {
            this.commandList = commandList;
            this.commandNestingLevels = commandNestingLevels;

        }

        string direction = "east";
        Point point = new Point();
        public void ExecuteCommands()
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], i, commandNestingLevels[i]);
            }
            Console.WriteLine("End state " + (point.X, point.Y) + " facing " + direction);
        }

        private void RunCommand(string cmd, int currentCommand, int currentNestingLevel)
        {
            if (cmd.Split(" ")[0] == "Repeat")
            {
                Repeat(cmd, currentCommand, currentNestingLevel); return;
            }
            else if (cmd.Split(" ")[0] == "Move")
            {
                Move(cmd); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "left")
            {
                TurnLeft(); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "right")
            {
                TurnRight(); return;
            }
        }

        public void Repeat(string cmd, int currentCommand, int currentNestinglevel)
        {
            int counter = 0;
            int othercount = 1;
            for (int j = 0; j < int.Parse(cmd.Split(" ")[1]); j++)
            {
                counter = 0;
                othercount = 1;
                foreach (int i in commandNestingLevels.Where(n => n > currentNestinglevel && commandList.Count > currentCommand + othercount))
                {
                    RunCommand(commandList[currentCommand + othercount], currentCommand + othercount, currentNestinglevel);
                    counter++;
                    othercount++;
                }
            }
            commandList.RemoveRange(currentCommand + 1, counter);
        }

        public void Move(string cmd)
        {
            int val = int.Parse(cmd.Split(" ")[1]);
            if (direction == "east")
            {
                point.X += val;
            }
            else if (direction == "north")
            {
                point.Y -= val;
            }
            else if (direction == "west")
            {
                point.X -= val;
            }
            else if (direction == "south")
            {
                point.Y += val;
            }
        }

        public void TurnLeft()
        {
            if (direction == "east")
            {
                direction = "north";
            }
            else if (direction == "north")
            {
                direction = "west";
            }
            else if (direction == "west")
            {
                direction = "south";
            }
            else if (direction == "south")
            {
                direction = "east";
            }
        }

        public void TurnRight()
        {
            if (direction == "east")
            {
                direction = "south";
            }
            else if (direction == "south")
            {
                direction = "west";
            }
            else if (direction == "west")
            {
                direction = "north";
            }
            else if (direction == "north")
            {
                direction = "east";
            }
        }
    }
}
