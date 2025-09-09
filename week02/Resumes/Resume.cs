using System;
using System.Collections.Generic;

public class Resume
{
    // Member variables (fields)
    public string _name;
    public List<Job> _jobs = new List<Job>();  // initialize list

    // Method to display resume details
    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}
