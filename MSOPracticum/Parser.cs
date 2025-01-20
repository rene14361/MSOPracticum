using MSOPracticumPresenter;
using System;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace MSOPracticum;

public class Parser : IComponent
{
    private Reader reader = new Reader();
    private bool detectedInvalid;
    private List<string> commandList = new List<string>();
    private List<int> commandNestingLevels = new List<int>();
    public string state { get; set; }
    public Presenter mediator { get; set; }


    public Parser()
    {
        AllocConsole(); // allocates a console window
        mediator = Presenter.GetPresenter();
    }

    // used to allocate a console window to this process
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public void Receive(string message)
    {

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
                input = Example.GetExample();
                break;

            case 3:
                input = state;
                break;

            case 4:
                // missing
                input = "";
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
            Console.WriteLine("Please adjust your input and run the app again.");
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

    private void CalculateMetrics(List<string> commandList, List<int> commandNestingLevels)
    {
        int commandAmount = commandList.Count;
        int maxNesting = commandNestingLevels.Max();
        int repeatAmount = 0;
        foreach (string command in commandList) if (command.Contains("Repeat")) repeatAmount++;

        Console.WriteLine($"These are the metrics of the commands:\nNumber of commands: {commandAmount}\nMaximum nesting level: {maxNesting}\nNumber of repeat commands: {repeatAmount}");
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
            v = int.Parse(comp.Split(" ")[1]);
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
        Command commands = new Command(commandList, commandNestingLevels);
        commands.ExecuteCommands();
    }
}