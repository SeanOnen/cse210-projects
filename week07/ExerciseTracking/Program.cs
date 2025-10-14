using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>();

        // Create a running activity
        Running run = new Running(new DateTime(2022, 11, 3), 30, 3.0);
        activities.Add(run);

        // Create a cycling activity
        Cycling cycle = new Cycling(new DateTime(2022, 11, 3), 30, 6.0);
        activities.Add(cycle);

        // Create a swimming activity
        Swimming swim = new Swimming(new DateTime(2022, 11, 3), 30, 60);
        activities.Add(swim);

        // Display summaries
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}