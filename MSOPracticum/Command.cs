using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOPracticum
{
    public class Command
    {
        List<string> commandList = new List<string>();
        public Command (List<string> commandList)
        {
            Point point = new Point();
            string direction = "east";
            this.commandList = commandList;
            for (int i = 0; i < commandList.Count; i++)
            {
                RunCommand(commandList[i], direction, point);
            }
            Console.WriteLine("End state " + (point.X, point.Y) + " facing " + direction);
        }

        public string comp;

        public void RunCommand(string cmd, string direction, Point point)
        {
            if (cmd.Split(" ")[0] == "Repeat")
            {
                Repeat(cmd); return;
            }
            else if (cmd.Split(" ")[0] == "Move")
            {
                Move(cmd, direction, point); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "left")
            {
                TurnLeft(direction); return;
            }
            else if (cmd.Split(" ")[0] == "Turn" && cmd.Split(" ")[1] == "right")
            {
                TurnRight(direction); return;
            }
        }

        public void Repeat(string cmd)
        {
            for (int j = 0; j < int.Parse(cmd.Split()[1]); j++)
            {

            }
        }

        public void Move(string cmd, string direction, Point point)
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

        public void TurnLeft(string direction)
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

        public void TurnRight(string direction)
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
