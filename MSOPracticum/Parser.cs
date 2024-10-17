using MSOPracticum;
using System.Runtime.CompilerServices;

class Parser
{
    private bool detectedInvalid;
    private List<string> commandList = new List<string>();

    public Parser()
    {
        
    }

    public void StartParser()
    {
        // if mode 1
        Reader reader = new Reader();
        string input = reader.EnterFilePath();
        ParseInput(input);
        Console.WriteLine(input);
        CallCommands(commandList);
        return;

        detectedInvalid = false;
        ParseInput(input);
        if (detectedInvalid)
        {
            Console.WriteLine("Please adjust your input and run the app again.");
            return;
        }

        //CallCommands(commandList);

    }

    private void ParseInput(string input)
    {
        List<string> splitInput = new List<string>(input.Split(" "));

        int counter = 0; // used to count the position in the original string for debug purposes
        // we use a while-loop here instead of a for-loop so that the length of the loop is being adjusted as the loop is running
        while(splitInput.Count > 0 && !detectedInvalid)
        {
            int i = 0; // index, resets to 0 with each loop
            // if there is no next string to pair the command with, mark the input as invalid.
            if (i + 1 > splitInput.Count - 1)
            {
                Console.WriteLine($"String \"" + splitInput[i] + "\" at index " + counter + " has no next string to form a command with.");
                detectedInvalid = true;
                return;
            }

            string tempCommand = splitInput[i] + " " + splitInput[i + 1];
            splitInput.RemoveAt(i); splitInput.RemoveAt(i);
;
            // if the temp command is invalid, there's still a chance it could be made valid by adding a third string from splitInput
            if (!IsValid(tempCommand))
            {
                // check whether a third string can be added
                if (i + 1 > splitInput.Count - 1)
                {
                    Console.WriteLine($"String \"" + tempCommand + "\" at index " + counter + " is invalid or has no next string to form a command with.");
                    detectedInvalid = true;
                    return;
                }
                
                tempCommand += " " + splitInput[i];
                splitInput.RemoveAt(i);

                // if the temp command is still invalid with a third added string, mark it as invalid.
                if (!IsValid(tempCommand))
                {
                    Console.WriteLine($"String \"" + tempCommand + "\" at index " + counter + " is invalid.");
                    detectedInvalid = true;
                    return;
                }
                counter++;
            }

            counter += 2;
            // no invalid input was detected, add it to list of commands.
            commandList.Add(tempCommand);
        }

        // successfully parsed with no invalid input
        Console.WriteLine("Done parsing.");
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

    private void CallCommands(List<string> commandList)
    {
        Command commands = new Command(commandList);
    }

}