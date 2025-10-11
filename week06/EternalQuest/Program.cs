/*
Exceeding the requirements by implementing a gamification element: a leveling system based on total score.
Every 1000 points, the user levels up and receives a new title, adding fun and motivation to the quest.
*/

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static List<Goal> goals = new List<Goal>();
    private static int score = 0;

    static void Main()
    {
        string file = "goals.txt";
        var (loadedGoals, loadedScore) = Load(file);
        goals = loadedGoals;
        score = loadedScore;
        bool quit = false;
        while (!quit)
        {
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.Write("Select: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Goal Types:");
                    Console.WriteLine("1 - Simple Goal");
                    Console.WriteLine("2 - Eternal Goal");
                    Console.WriteLine("3 - Checklist Goal");
                    Console.Write("Which type? ");
                    string gtype = Console.ReadLine();
                    Console.Write("Description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Points for each completion: ");
                    int points = int.Parse(Console.ReadLine());
                    Goal newGoal = null;
                    if (gtype == "1")
                    {
                        newGoal = new SimpleGoal(desc, points);
                    }
                    else if (gtype == "2")
                    {
                        newGoal = new EternalGoal(desc, points);
                    }
                    else if (gtype == "3")
                    {
                        Console.Write("Times to complete: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus points: ");
                        int bonus = int.Parse(Console.ReadLine());
                        newGoal = new ChecklistGoal(desc, points, target, bonus);
                    }
                    else
                    {
                        Console.WriteLine("Invalid type.");
                        continue;
                    }
                    goals.Add(newGoal);
                    Console.WriteLine("Goal created!");
                    break;
                case "2":
                    Console.WriteLine("\nGoals:");
                    for (int i = 0; i < goals.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {goals[i].GetCompletionString()} - {goals[i].Description}");
                    }
                    if (goals.Count == 0) Console.WriteLine("No goals yet.");
                    break;
                case "3":
                    if (goals.Count == 0)
                    {
                        Console.WriteLine("No goals.");
                        break;
                    }
                    Console.WriteLine("\nGoals:");
                    for (int i = 0; i < goals.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {goals[i].GetCompletionString()} - {goals[i].Description}");
                    }
                    Console.Write("Which goal did you complete? ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
                    {
                        index--;
                        int awarded = goals[index].RecordEvent();
                        score += awarded;
                        Console.WriteLine($"Congratulations! You've earned {awarded} points.");
                        if (awarded > goals[index].Points)
                        {
                            Console.WriteLine("Special bonus awarded!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal.");
                    }
                    break;
                case "4":
                    int level = score / 1000;
                    string[] levelTitles = { "Mortal", "Seeker", "Disciple", "Acolyte", "Sage", "Prophet", "Apostle", "Saint", "Archangel", "Deity" };
                    string currentTitle = level < levelTitles.Length ? levelTitles[level] : "Eternal Deity";
                    Console.WriteLine($"\nYour current score: {score} points");
                    Console.WriteLine($"Level: {level} - {currentTitle}");
                    break;
                case "5":
                    Save(goals, score, file);
                    Console.WriteLine("Goals saved.");
                    break;
                case "6":
                    var (newGoals, newScore) = Load(file);
                    goals = newGoals;
                    score = newScore;
                    Console.WriteLine("Goals loaded.");
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static (List<Goal>, int) Load(string file)
    {
        List<Goal> loadedGoals = new List<Goal>();
        int loadedScore = 0;
        if (!File.Exists(file)) return (loadedGoals, loadedScore);
        try
        {
            using (StreamReader reader = new StreamReader(file))
            {
                loadedScore = int.Parse(reader.ReadLine());
                int numGoals = int.Parse(reader.ReadLine());
                for (int i = 0; i < numGoals; i++)
                {
                    string type = reader.ReadLine().Trim();
                    string description = reader.ReadLine().Trim();
                    int points = int.Parse(reader.ReadLine());
                    Goal goal = null;
                    switch (type)
                    {
                        case "Simple":
                            bool completed = int.Parse(reader.ReadLine()) == 1;
                            goal = new SimpleGoal(description, points, completed);
                            break;
                        case "Eternal":
                            goal = new EternalGoal(description, points);
                            break;
                        case "Checklist":
                            int target = int.Parse(reader.ReadLine());
                            int bonus = int.Parse(reader.ReadLine());
                            int completedCount = int.Parse(reader.ReadLine());
                            goal = new ChecklistGoal(description, points, target, bonus, completedCount);
                            break;
                    }
                    if (goal != null) loadedGoals.Add(goal);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Load error: {e.Message}");
        }
        return (loadedGoals, loadedScore);
    }

    private static void Save(List<Goal> savedGoals, int savedScore, string file)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(savedScore);
                writer.WriteLine(savedGoals.Count);
                foreach (Goal goal in savedGoals)
                {
                    if (goal is SimpleGoal)
                    {
                        writer.WriteLine("Simple");
                        writer.WriteLine(goal.Description);
                        writer.WriteLine(goal.Points);
                        writer.WriteLine(goal.IsComplete() ? 1 : 0);
                    }
                    else if (goal is EternalGoal)
                    {
                        writer.WriteLine("Eternal");
                        writer.WriteLine(goal.Description);
                        writer.WriteLine(goal.Points);
                    }
                    else if (goal is ChecklistGoal checklist)
                    {
                        writer.WriteLine("Checklist");
                        writer.WriteLine(goal.Description);
                        writer.WriteLine(goal.Points);
                        writer.WriteLine(checklist.Target);
                        writer.WriteLine(checklist.Bonus);
                        writer.WriteLine(checklist.CompletedCount);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Save error: {e.Message}");
        }
    }
}