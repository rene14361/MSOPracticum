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
public class Program
{
    static void Main(string[] args)
    {
        Program program = new Program();
        Console.WriteLine("Welcome to our Programming Learning App!\nPlease enter \u001b[1m1\u001b[0m if you want to import your own commands, or \u001b[1m2\u001b[0m if you want to use example commands.");
        bool modeSelected = false;
        while (!modeSelected)
        {
            modeSelected = program.ChooseMode();
        }

    }

    // Method responsible for making the player choose a mode. Starts the corresponding mode and returns a bool denoting whether a mode was selected succesfully.
    private bool ChooseMode()
    {
        string input = Console.ReadLine();
        int number = 0;
        int.TryParse(input, out number);

        switch (number)
        {
            // temporary implementation
            case 1:
                Reader reader = new Reader();
                return true;

            case 2:
                // not implemented
                return true;

            default:
                Console.WriteLine("Invalid input, please enter \u001b[1m1\u001b[0m or \u001b[1m2\u001b[0m.");
                return false;
        }
    }
}