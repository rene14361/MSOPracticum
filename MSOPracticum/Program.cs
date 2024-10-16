using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;

namespace MSOPracticum
{
    class Program
    {
        static void Main()
        {
            Command cmd = new();
            cmd.comp = new List<string>();
            Point point = new Point();
            point.X = 0; point.Y = 0;
            string direction = "east";
            while (Console.ReadLine() != "End")
            {
                string line = Console.ReadLine();
                if (cmd.IsValid(line))
                {
                    cmd.comp.Add(line);
                }
                else { Console.WriteLine("Error, invalid command, please try again"); }
            }
            for (int i = 0; i < cmd.comp.Count; i++)
            {
                if (cmd.comp[i].Split(" ")[0] == "Repeat")
                {
                    for (int j = 0; j < int.Parse(cmd.comp[i].Split()[1]); j++)
                    {
                        
                    }
                }
                else if (cmd.comp[i].Split(" ")[0] == "Move")
                {
                    int val = int.Parse(cmd.comp[i].Split(" ")[1]);
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
                else if (cmd.comp[i].Split(" ")[0] == "Turn" && cmd.comp[i].Split(" ")[1] == "left")
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
                else if (cmd.comp[i].Split(" ")[0] == "Turn" && cmd.comp[i].Split(" ")[1] == "right")
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
                Console.WriteLine(cmd.comp[i]);
            }
            Console.WriteLine("End state " + (point.X, point.Y) + " facing " + direction);
        }
    }
}