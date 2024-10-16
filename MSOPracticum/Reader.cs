using System.Runtime.CompilerServices;

class Reader
{
    public string input;

    // Constructor which automatically starts the file reading process.
    public Reader()
    {
        EnterFilePath();
    }

    // Method responsible for getting the user to enter a file path until a file is successfully read.
    private void EnterFilePath()
    {
        Console.WriteLine("Please enter the full path of the text file.\nAn example: C:\\Users\\User\\Documents\\sample.txt\nPlease note that a space will be added for each new line (besides those before the first character) while reading your text file.");
        bool successfulRead = false;
        while (!successfulRead)
        {
            string path = Console.ReadLine();
            successfulRead = TryRead(path);
        }
    }
    
    // Tries to read the file at the provided path, returns a bool denoting whether the file was successfully read.
    private bool TryRead(string path)
    {
        try
        {
            StreamReader streamReader = new StreamReader(path);
            string line, text = String.Empty;
            while((line = streamReader.ReadLine()) != null)
            {
                // checks whether text string is empty to avoid adding a space to the beginning of the string
                if (text == String.Empty) text = line;
                // if it's not empty add the space and the next line
                else text += " " + line;
            }
            input = text;
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            Console.WriteLine("Please try again.");
            return false;
        }
    }
}