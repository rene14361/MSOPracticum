using System.Runtime.CompilerServices;

class Reader
{
    private string userInput; // the string that is the result of reading the file

    public Reader()
    {

    }

    // Method responsible for getting the user to enter a file path until a file is successfully read.
    public string EnterFilePath()
    {
        Console.WriteLine("Please enter the full path of the text file.\nAn example: C:\\Users\\User\\Documents\\sample.txt\nPlease note that a space will be added for each new line (besides those before the first character) while reading your text file.");
        bool successfulRead = false;
        while (!successfulRead)
        {
            string path = Console.ReadLine();
            successfulRead = TryRead(path);
        }
        return userInput;
    }
    
    // Tries to read the file at the provided path, returns a bool denoting whether the file was successfully read.
    private bool TryRead(string path)
    {
        try
        {
            userInput = Read(path);
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            Console.WriteLine("Please try again.");
            return false;
        }
    }

    // Reads the text file at the place denoted by the file path and converts it into a single string. Automatically adds spaces for each new line, except at the beginning.
    public string Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        string line, text = String.Empty;
        while ((line = streamReader.ReadLine()) != null)
        {
            // checks whether text string is empty to avoid adding a space to the beginning of the string
            if (text == String.Empty) { text = line; }
            // if it's not empty add the space and the next line
            else { text += " " + line; }
        }
        return text;
    }
}