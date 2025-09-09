using System;

public class Entry
{
    public string _date;        // store date as string for simplicity
    public string _prompt;      // the prompt question
    public string _response;    // user response

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_prompt}");
        Console.WriteLine(_response);
        Console.WriteLine(); // blank line
    }

    // Return a string suitable for saving to a file
    public string ToFileString()
    {
        return $"{_date}|{_prompt}|{_response}";
    }

    // Create an Entry from a line in the file
    public static Entry FromFileString(string line)
    {
        string[] parts = line.Split('|');
        return new Entry
        {
            _date = parts[0],
            _prompt = parts[1],
            _response = parts[2]
        };
    }
}
