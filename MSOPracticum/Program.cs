namespace MSOPracticum
{
    class Program
    {
        static void Main()
        {
            Program program = new Program();
            Console.WriteLine("Welcome to our Programming Learning App!");
            int mode = ChooseInputMode();
            int metrics = ChooseMetricsMode();
            Start(mode, metrics);
        }

        private static int ChooseInputMode()
        {
            Console.WriteLine("Please enter \u001b[1m1\u001b[0m if you want to import your own commands, or \u001b[1m2\u001b[0m if you want to use example commands.");
            // this works because a valid input breaks the loop by invoking return
            while (true)
            {
                string input = Console.ReadLine();
                int number = 0;
                int.TryParse(input, out number);

                switch (number)
                {
                    case int i when i > 0 && i < 3:
                        return number;

                    default:
                        Console.WriteLine("Invalid input, please enter \u001b[1m1\u001b[0m or \u001b[1m2\u001b[0m.");
                        break;
                }
            }
        }

        private static int ChooseMetricsMode()
        {
            Console.WriteLine("Please enter \u001b[1m1\u001b[0m if you want to execute the program normally, \u001b[1m2\u001b[0m if you just want the metrics, or \u001b[1m3\u001b[0m if you want both.");
            // this works because a valid input breaks the loop by invoking return
            while (true)
            {
                string input = Console.ReadLine();
                int number = 0;
                int.TryParse(input, out number);

                switch (number)
                {
                    case int i when i > 0 && i < 4:
                        return number;

                    default:
                        Console.WriteLine("Invalid input, please enter \u001b[1m1\u001b[0m, \u001b[1m2\u001b[0m or \u001b[1m3\u001b[0m.");
                        break;
                }
            }
        }

        private static void Start(int mode, int metrics) 
        {
            Parser parser = new Parser();
            parser.StartParser(mode, metrics);
        }
    }
}