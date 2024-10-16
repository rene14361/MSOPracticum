using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;

namespace MSOPracticum
{
    class Program
    {
        static void Main()
        {
            Program program = new Program();
            Console.WriteLine("Welcome to our Programming Learning App!\nPlease enter \u001b[1m1\u001b[0m if you want to import your own commands, or \u001b[1m2\u001b[0m if you want to use example commands.");
            bool modeSelected = false;
            while (!modeSelected)
            {
                modeSelected = program.ChooseMode();
            }
            //Command cmd = new();
            //cmd.comp = null;
            //Point point = new Point();
            //point.X = 0; point.Y = 0;
            //string direction = "east";
            //while (Console.ReadLine() != "End")
            //{
            //    string line = Console.ReadLine();
            //    if (cmd.IsValid(line))
            //    {
            //        cmd.comp = line;
            //        cmd.RunCommand(cmd.comp, direction, point);
            //        Console.WriteLine(cmd.comp);
            //    }
            //    else { Console.WriteLine("Error, invalid command, please try again"); }
            //}
            //Console.WriteLine("End state " + (point.X, point.Y) + " facing " + direction);
        }
        private bool ChooseMode()
        {
            string input = Console.ReadLine();
            int number = 0;
            int.TryParse(input, out number);

            switch (number)
            {
                // temporary implementation
                case 1:
                    Parser parser = new Parser();
                    parser.StartParser();
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

}