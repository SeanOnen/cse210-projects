
using System;
using System.Threading;

namespace MindfulnessApp
{
    public abstract class MindfulnessActivity
    {
        protected string _name;
        protected string _description;
        protected int _duration;

        public MindfulnessActivity(string n, string d)
        {
            _name = n;
            _description = d;
        }

        public string Name => _name;
        public string Description => _description;
        public int Duration 
        { 
            get => _duration; 
            set => _duration = value; 
        }

        public virtual int DisplayStartMessage()
        {
            Console.WriteLine($"\n=== {Name} Activity ===");
            Console.WriteLine(Description);
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
            _duration = 30; // Default
            if (int.TryParse(input, out int parsedDuration))
            {
                _duration = parsedDuration;
            }
            else
            {
                Console.WriteLine("Invalid input. Using default duration of 30 seconds.");
            }
            Console.WriteLine("\nGet ready...");
            AnimatePause(3, AnimationType.Countdown);
            return _duration;
        }

        public virtual void DisplayEndMessage()
        {
            Console.WriteLine("\nWell done!!");
            AnimatePause(2, AnimationType.Spinner);
            Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
            AnimatePause(3, AnimationType.Countdown);
        }

        public abstract void Perform();

        protected virtual void AnimatePause(int seconds, AnimationType type)
        {
            switch (type)
            {
                case AnimationType.Countdown:
                    for (int i = seconds; i > 0; i--)
                    {
                        string num = i.ToString();
                        int len = num.Length;
                        Console.Write(num);
                        Thread.Sleep(1000);
                        string backspaces = new string('\b', len);
                        string spaces = new string(' ', len);
                        Console.Write(backspaces + spaces + backspaces);
                    }
                    break;
                case AnimationType.Spinner:
                    char[] chars = { '|', '/', '-', '\\' };
                    for (int s = 0; s < seconds; s++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            char c = chars[j];
                            Console.Write(c);
                            Thread.Sleep(250);
                            Console.Write('\b');
                        }
                    }
                    Console.Write(" \b");
                    break;
                case AnimationType.Dots:
                    for (int i = 0; i < seconds; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine();
                    break;
            }
        }
    }
}