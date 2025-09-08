using System;
using System.Collections.Generic;
using System.Linq;  // For Sum(), Average(), Min(), Max()

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int num = int.Parse(Console.ReadLine());

            if (num == 0)
                break; // stop input loop

            numbers.Add(num);
        }

        // --- Core Requirements ---
        int sum = numbers.Sum();
        double average = numbers.Average();
        int largest = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");

        // --- Stretch Challenge ---
        // Find smallest positive number
        var positives = numbers.Where(n => n > 0);
        if (positives.Any())
        {
            int smallestPositive = positives.Min();
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Sort numbers
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }
    }
}
