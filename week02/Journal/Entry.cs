// --------------------------------------------------------------
// Exceeded Requirements:
// 1. Added helper methods ToFileString and FromFileString so
//    serialization and deserialization of entries are handled
//    in one place. This keeps Program.cs and Journal.cs cleaner.
// 2. Works with CSV/quoted format so entries can be opened in Excel.
// --------------------------------------------------------------

using System;

public class Entry
{
    public string _date;        // store date as string for simplicity
    public string _prompt;      // the prompt question
    public string _response;    // user response

    // Display the entry nicely
    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_prompt}");
        Console.WriteLine(_response);
        Console.WriteLine(); // blank line
    }

    // Return a string suitable for saving to a file (pipe-delimited or CSV)
    public string ToFileString()
    {
        // if using CSV, escape quotes and wrap in quotes
        string date = _date.Replace("\"", "\"\"");
        string prompt = _prompt.Replace("\"", "\"\"");
        string response = _response.Replace("\"", "\"\"");

        return $"\"{date}\",\"{prompt}\",\"{response}\"";
    }

    // Create an Entry from a line in the file (CSV version)
    public static Entry FromFileString(string line)
    {
        // Very simple CSV split (use same ParseCsvLine from Journal.cs if needed)
        string[] parts = line.Split("\",\"");
        // Clean up quotes at edges
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = parts[i].Trim('"');
        }

        return new Entry
        {
            _date = parts.Length > 0 ? parts[0] : "",
            _prompt = parts.Length > 1 ? parts[1] : "",
            _response = parts.Length > 2 ? parts[2] : ""
        };
    }
}
