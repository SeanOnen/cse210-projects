// --------------------------------------------------------------
// Exceeded Requirements:
// 1. Journal saves and loads entries in proper CSV/quoted format
//    so the file can be opened directly in Excel without breaking
//    on commas or quotes inside the text.
// 2. Uses Entry.ToFileString and Entry.FromFileString methods for
//    serialization and deserialization, keeping the code clean.
// --------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    // Add an entry
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Display all entries
    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    // Save journal to a CSV file
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {
                // Write entry using its own helper
                writer.WriteLine(e.ToFileString());
            }
        }
        Console.WriteLine($"Journal saved to {filename}");
    }

    // Load journal from a CSV file
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear(); // Replace any current entries
        foreach (string line in File.ReadAllLines(filename))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Entry e = Entry.FromFileString(line);
                _entries.Add(e);
            }
        }
        Console.WriteLine($"Journal loaded from {filename}");
    }
}
