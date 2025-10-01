
using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ListingActivity : MindfulnessActivity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void Perform()
        {
            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];
            Console.WriteLine($"\n{prompt}");
            Console.WriteLine("You have 5 seconds to prepare...");
            AnimatePause(5, AnimationType.Countdown);

            DateTime startTime = DateTime.Now;
            List<string> items = new List<string>();
            Console.WriteLine("\nStart listing items (press Enter after each, or type 'done' to finish early):");

            while ((DateTime.Now - startTime).TotalSeconds < Duration)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (line != null && line.ToLower() == "done")
                {
                    break;
                }
                if (!string.IsNullOrWhiteSpace(line))
                {
                    items.Add(line);
                }
            }

            Console.WriteLine($"\nYou listed {items.Count} items.");
        }
    }
}