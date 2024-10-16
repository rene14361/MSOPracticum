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