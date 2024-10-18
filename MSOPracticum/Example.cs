namespace MSOPracticum;

public class Example
{
    private static string example1 = "Move 5 Turn left Move 2 Turn right Move 10 Turn right Move 2 Move 4 Turn left Turn left Move 1";
    private static string example2 = "Move 2 Repeat 5 times \tMove 2 \tTurn left \tMove 1 Turn right Move 1";
    private static string example3 = "Turn right Move 1 Repeat 2 times \tMove 1 \tTurn right \tRepeat 5 times \t\tMove 2 \t\tTurn right \tMove 5 Turn left Repeat 3 times \tMove 1";

    public static string GetExample()
    {
        Console.WriteLine("Please enter the number corresponding to the example you want to use:\n\u001b[1m1\u001b[0m - Basic; \u001b[1m2\u001b[0m - Advanced; \u001b[1m3\u001b[0m - Expert; \u001b[1m4\u001b[0m - Pick a random one");
        // this works because a valid input breaks the loop by invoking return
        while (true)
        {
            string input = Console.ReadLine();
            int number = 0;
            int.TryParse(input, out number);
            switch (number)
            {
                case 1:
                    return example1;

                case 2:
                    return example2;

                case 3:
                    return example3;

                case 4:
                    Random rnd = new Random();
                    return ReturnExample(rnd.Next(1, 3));

                default:
                    Console.WriteLine("Invalid input, please enter \u001b[1m1\u001b[0m, \u001b[1m2\u001b[0m, \u001b[1m3\u001b[0m or \u001b[1m4\u001b[0m.");
                    break;
            }
        }
    }

    private static string ReturnExample(int n)
    {
        switch (n)
        {
            case 1:
                return example1;

            case 2:
                return example2;

            case 3:
                return example3;

            default:
                return "Invalid example";

        }
    }
}