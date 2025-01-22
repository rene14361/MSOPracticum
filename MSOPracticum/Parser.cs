using System.Runtime.InteropServices;

namespace MSOPracticum;

public class Parser : IComponent
{
    private Reader reader = new Reader();
    private bool detectedInvalid;
    private bool exerciseMode = false;
    private bool[,] exerciseGrid = new bool[5, 5];
    private Point exerciseGoal = new Point(0, 0);
    private List<string> commandList = new List<string>();
    private List<int> commandNestingLevels = new List<int>();
    private Presenter mediator { get; set; }
    private string request { get; set; }
    public bool usingUI = true; // only exists to support running the old console program by setting it to false, we're not sure whether backwards compatibility is required, if it's not then pretend this doesn't exist

    public Parser(Presenter presenter)
    {
        AllocConsole(); // Allocates a console window
        mediator = presenter;
        mediator.ParserComponent = this;
    }

    public void Receive(string message)
    {
        string[] splitMessage = message.Split("|");

        switch (splitMessage[0])
        {
            case "Load" or "Exercise":
                // Asks the reader to load the contents of a file and sends these contents back to the mediator
                Console.WriteLine("Loading file at path: " + splitMessage[1]);
                if (reader.TryRead(splitMessage[1]))
                {
                    string text;
                    text = reader.Read(splitMessage[1], "\r\n");

                    // if task is unrelated to exercise, notify mediator and return
                    if (splitMessage[0] != "Exercise") { mediator.Notify(this, splitMessage[0] + "|" + text); return; }

                    // tries to parse exercise, returns if unsuccessful and notifies mediator if successful
                    if (!ParseExercise(text)) return;
                    string exercise = "";
                    foreach (bool value in exerciseGrid) exercise += value ? "True," : "False,";
                    mediator.Notify(this, $"Exercise|{exercise}|{exerciseGoal.X},{exerciseGoal.Y}");
                }
                else Console.WriteLine("Please adjust your file path.");
                break;

            case "Parse" or "Attempt":
                int metrics;
                int.TryParse(splitMessage[1], out metrics);

                // Selects parsing mode 3 for text input and parsing mode 4 for custom file paths
                int mode = splitMessage[2] switch
                {
                    "Custom" or "Basic" or "Advanced" or "Expert" => 3,
                    _ => 4
                };
                
                if (mode == 3) request = splitMessage[3];
                else request = splitMessage[2];

                // Resets the command list, command nesting levels and exercise mode flag so that if this is not the first time the program is run it doesn't stack the new input with previous ones
                commandList = new List<string>();
                commandNestingLevels = new List<int>();
                exerciseMode = false;

                if (splitMessage[0] == "Attempt") exerciseMode = true;

                ExecuteParser(mode, metrics);
                break;

            // Returns example commands that correspond to selection
            case "Example":
                int n = splitMessage[1] switch
                {
                    "Basic" => 1,
                    "Advanced" => 2,
                    "Expert" => 3,
                    _ => 0
                };
                if (n == 0) return;
                else mediator.Notify(this, $"{splitMessage[0]}|{Example.ReturnExample(n)}" );
                break;
        }
    }

    public void ExecuteParser(int mode, int metrics)
    {
        string input;
        switch (mode)
        {
            case 1:
                input = reader.EnterFilePath();
                break;

            case 2:
                input = Example.GetExample().Replace(Environment.NewLine, " ");
                break;

            case 3:
                input = request.Replace(Environment.NewLine, " ");
                break;

            case 4:
                if (reader.TryRead(request)) input = reader.Read(request, " ");
                else { Console.WriteLine("Please adjust your file path and run the program again."); return; }
                break;

            default:
                Console.WriteLine("Mode is incorrect, defaulting to mode 1.");
                input = reader.EnterFilePath();
                break;
        }

        detectedInvalid = false;
        ParseInput(input);
        if (detectedInvalid)
        {
            Console.WriteLine("Please adjust your input and run the program again.");
            return;
        }

        // if metrics mode is 2 or 3, calculate metrics
        if (metrics >= 2) CalculateMetrics(commandList, commandNestingLevels);
        // if metrics mode is 1 or 3, go on with the program
        if (metrics != 2) CallCommands(commandList, commandNestingLevels);
    }

    private void ParseInput(string input)
    {
        Console.WriteLine("Started parsing. Please note that our parser is case sensitive and assumes that you're using tabs for nesting, not 4/8 spaces.");

        // note: both of these lists end up being of equal length
        List<string> splitInput = new List<string>(input.Split(" "));
        List<int> inputNestingLevels = new List<int>();

        // for-loop responsible for counting and removing tabs from each string
        for (int i = 0; i < splitInput.Count; i++)
        {
            int tabs = 0;

            // each tab is considered a single whitespace in C#, so this works
            foreach (char c in splitInput[i]) if (Char.IsWhiteSpace(c)) tabs++;
            inputNestingLevels.Add(tabs);

            // trims the whitespace from the strings since it's no longer needed and enables later conditionals to work correctly
            splitInput[i] = splitInput[i].Trim();
        }

        int counter = 0; // used to count the position in the original string for debug purposes
        // we use a while-loop here instead of a for-loop so that the length of the loop is being adjusted as the loop is running
        while (splitInput.Count > 0 && !detectedInvalid)
        {
            int i = 0; // index always 0, used to interact with splitInput
            // if there is no next string to pair the command with, mark the input as invalid.
            if (i + 1 > splitInput.Count - 1)
            {
                Console.WriteLine($"String \"{splitInput[i]}\" at index {counter} has no next string to form a command with.");
                detectedInvalid = true;
                return;
            }

            string tempCommand = splitInput[i] + " " + splitInput[i + 1];
            splitInput.RemoveAt(i); splitInput.RemoveAt(i);
            // make the nesting level of the first word the nesting level of the command
            int tempCommandNesting = inputNestingLevels[i];
            inputNestingLevels.RemoveAt(i); inputNestingLevels.RemoveAt(i);

            // if the temp command is invalid, there's still a chance it could be made valid by adding a third string from splitInput
            if (!IsValid(tempCommand))
            {
                // check whether a third string can be added
                if (i + 1 > splitInput.Count - 1)
                {
                    Console.WriteLine($"String \"{tempCommand}\" at index {counter} is invalid or has no next string to form a command with.");
                    detectedInvalid = true;
                    return;
                }

                tempCommand += " " + splitInput[i];
                splitInput.RemoveAt(i);
                inputNestingLevels.RemoveAt(i);

                // if the temp command is still invalid with a third added string, mark it as invalid.
                if (!IsValid(tempCommand))
                {
                    Console.WriteLine($"String \"{tempCommand}\" at index {counter} is invalid.");
                    detectedInvalid = true;
                    return;
                }
                counter++;
            }

            counter += 2;
            // no invalid input was detected, add the command and the nesting level to list of commands and nesting levels.
            commandList.Add(tempCommand);
            commandNestingLevels.Add(tempCommandNesting);
        }

        // successfully parsed with no invalid input
        Console.WriteLine("Done parsing.");
    }

    private bool ParseExercise(string characters)
    {
        int x = 0; int y = 0;
        characters = characters.Replace("\r\n", "");

        // guard statement to get rid of invalid length grids
        if (characters.Length != 25)
        {
            Console.WriteLine("Invalid input, exercise grid is not the allowed size of 25 (5x5).");
            return false;
        }

        bool goalFound = false;
        // validates each character and assigns the corresponding bool value in the grid
        foreach (char c in characters)
        {
            if (c == '+') exerciseGrid[x, y] = false;
            else if (c == 'o') exerciseGrid[x, y] = true;
            else if (c == 'x')
            {
                goalFound = true;
                exerciseGoal = new Point(x, y);
                exerciseGrid[x, y] = true;
            }
            else
            {
                Console.WriteLine($"The character '{c}' is invalid, please adjust your exercise.");
                return false;
            }
            if (y < 4) y++;
            else { y = 0; x++; }
        }
        if (goalFound) return true;
        Console.WriteLine("There was no goal included in the grid, please adjust your exercise.");
        return false;
    }

    private void CalculateMetrics(List<string> commandList, List<int> commandNestingLevels)
    {
        int commandAmount = commandList.Count;
        int maxNesting = commandNestingLevels.Max();
        int repeatAmount = 0;
        foreach (string command in commandList) if (command.Contains("Repeat")) repeatAmount++;

        string metricOutput = $"Number of commands: {commandAmount}\r\nMaximum nesting level: {maxNesting}\r\nNumber of repeat commands: {repeatAmount}";
        Console.WriteLine(metricOutput);
        if (!usingUI) return;
        mediator.Notify(this, "Metrics|" + metricOutput);
    }

    private bool IsValid(string comp)
    {
        var v = 0;
        if (comp.Split(" ").Count() <= 1 || comp.Split().Count() > 3)
        {
            return false;
        }
        if (comp.Split(" ")[0] == "Move" || comp.Split(" ")[0] == "Repeat")
        {
            if (!int.TryParse(comp.Split(" ")[1], out v)) return false;
        }
        if (comp != null)
        {
            if (comp == "Turn left" || comp == "Turn right")
                return true;
            else if (comp.Split(" ")[0] == "Move")
                return true;
            else if (comp.Split().Count() < 3)
                return false;
            else if (comp.Split(" ")[0] == "Repeat"
                     && comp.Split(" ")[2] == "times"
                     && int.TryParse(comp.Split(" ")[1], result: out v))
            { return true; }
            else { return false; }
        }
        else { return false; }
    }

    private void CallCommands(List<string> commandList, List<int> commandNestingLevels)
    {
        Command commands = new Command(mediator, commandList, commandNestingLevels, exerciseGrid, exerciseGoal);
        if (exerciseMode) mediator.Notify(this, "Mode");
        if (!usingUI) commands.usingUI = false;
        commands.ExecuteCommands();
    }

    // Used to allocate a console window to this process
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();
}