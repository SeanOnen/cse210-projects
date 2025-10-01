
#nullable enable

using System;
using System.Collections.Generic;
using System.IO;

namespace MindfulnessApp
{
    /*
     To exceed requirements, I added the following features:
     1. Logging of each completed activity to a file named 'mindfulness_log.txt'. Each log entry includes the timestamp, activity name, and duration of the session. This provides persistent tracking of user activity without requiring manual statistics computation.
     2. In the ReflectionActivity, questions are selected randomly without repetition until all questions have been used at least once in the session. After all questions are used, the selection cycles back, ensuring variety and completeness in reflection prompts.
     3. Enhanced animation support with a Dots type (though not used in core activities, available for future extension), demonstrating abstraction in the base class.
     These additions go beyond the core functional requirements by providing persistence, improved randomization logic, and extensibility while maintaining encapsulation (private lists) and inheritance principles.
    */

    class Program
    {
        private static void LogActivity(string activityName, int duration)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Completed {activityName} for {duration} seconds{Environment.NewLine}";
            File.AppendAllText("mindfulness_log.txt", logEntry);
        }

        static void Main(string[] args)
        {
            Dictionary<int, MindfulnessActivity> activities = new Dictionary<int, MindfulnessActivity>
            {
                {1, new BreathingActivity()},
                {2, new ReflectionActivity()},
                {3, new ListingActivity()}
            };

            Console.WriteLine("Welcome to the Mindfulness Program!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Starting Breathing Activity");
                Console.WriteLine("2. Starting Reflection Activity");
                Console.WriteLine("3. Starting Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Select a choice from the menu: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 4)
                    {
                        Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                        break;
                    }

                    if (activities.TryGetValue(choice, out MindfulnessActivity? activity))
                    {
                        int duration = activity.DisplayStartMessage();
                        activity.Duration = duration;
                        activity.Perform();
                        activity.DisplayEndMessage();
                        LogActivity(activity.Name, activity.Duration);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please select 1-4.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number 1-4.");
                }
            }
        }
    }
}