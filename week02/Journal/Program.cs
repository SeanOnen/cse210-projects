// --------------------------------------------------------------
// Exceeded Requirements:
// 1. The Journal saves and loads entries in a CSV/quoted format
//    so the file can be opened directly in Excel without breaking
//    on commas or quotes inside the text.
// 2. Added clear comments and helper methods to keep the code
//    organized and maintainable.
// --------------------------------------------------------------

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // List of possible prompts to randomly show to the user
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        Journal journal = new Journal();
        string choice = "";

        // Main menu loop
        while (choice != "5")
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": // Write new entry
                    Random rand = new Random();
                    string prompt = prompts[rand.Next(prompts.Count)];
                    Console.WriteLine(prompt);
                    Console.Write("> ");
                    string response = Console.ReadLine();

                    Entry entry = new Entry
                    {
                        _date = DateTime.Now.ToShortDateString(),
                        _prompt = prompt,
                        _response = response
                    };
                    journal.AddEntry(entry);
                    break;

                case "2": // Display journal
                    journal.Display();
                    break;

                case "3": // Save to file
                    Console.Write("Enter filename to save to: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "4": // Load from file
                    Console.Write("Enter filename to load from: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
