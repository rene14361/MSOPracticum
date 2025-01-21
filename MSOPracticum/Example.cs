namespace MSOPracticum;

public class Example
{   
    private static string example1 = "Move 5\r\nTurn left\r\nMove 2\r\nTurn right\r\nMove 10\r\nTurn right\r\nMove 2\r\nMove 4\r\nTurn left\r\nTurn left\r\nMove 1";
    private static string example2 = "Move 2\r\nRepeat 5 times\r\n\tMove 2\r\n\tTurn left\r\n\tMove 1\r\nTurn right Move 1";
    private static string example3 = "Turn right\r\nMove 1\r\nRepeat 2 times\r\n\tMove 1\r\n\tTurn right\r\n\tRepeat 5 times\r\n\t\tMove 2\r\n\t\tTurn right\r\n\tMove 5\r\nTurn left\r\nRepeat 3 times\r\n\tMove 1";

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

    public static string ReturnExample(int n)
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