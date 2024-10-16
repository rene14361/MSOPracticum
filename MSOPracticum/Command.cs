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
        public Command() { }
        public string comp;

        public bool IsValid(string comp)
        {
            var v = 0;
            if (comp.Split(" ").Count() <= 1 || comp.Split().Count() > 3)
            {
                return false;
            }
            if (comp.Split(" ")[0] == "Move" || comp.Split(" ")[0] == "Repeat")
            {
                v = int.Parse(comp.Split(" ")[1]);
            }
            if (comp != null)
            {
                if (comp == "Turn left" || comp == "Turn right")
                { return true; }
                else if (comp.Split(" ")[0] == "Move")
                { return true; }
                else if (comp.Split().Count() < 3)
                { return false; }
                else if (comp.Split(" ")[0] == "Repeat"
                         && comp.Split(" ")[2] == "times"
                         && int.TryParse(comp.Split(" ")[1], result: out v))
                { return true; }
                else { return false; }
            }
            else { return false; }
        }
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
