﻿namespace MSOPracticum;

class Reader
{
    private string fileContents; // the string that is the result of reading the file

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
        return fileContents;
    }
    
    // Tries to read the file at the provided path, returns a bool denoting whether the file was successfully read.
    public bool TryRead(string path)
    {
        try
        {
            fileContents = Read(path, " ");
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            Console.WriteLine("Wrong file path, please try again.");
            return false;
        }
    }

    // Reads the text file at the place denoted by the file path and converts it into a single string. Automatically adds spaces for each new line, except at the beginning.
    public string Read(string path, string filler)
    {
        StreamReader streamReader = new StreamReader(path);
        string line, text = String.Empty;
        while ((line = streamReader.ReadLine()) != null)
        {
            // checks whether text string is empty to avoid adding a space to the beginning of the string
            if (text == String.Empty) { text = line; }
            // if it's not empty add the filler string and the next line
            else { text += filler + line; }
        }
        return text;
    }
}