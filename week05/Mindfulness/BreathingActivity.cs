
using System;

namespace MindfulnessApp
{
    public class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void Perform()
        {
            DateTime startTime = DateTime.Now;
            bool breathingIn = true;
            while ((DateTime.Now - startTime).TotalSeconds < Duration)
            {
                Console.WriteLine(breathingIn ? "\nBreathe in..." : "\nBreathe out...");
                AnimatePause(4, AnimationType.Countdown);
                breathingIn = !breathingIn;
            }
        }
    }
}