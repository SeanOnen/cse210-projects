using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());

        string letter;

        if (grade >= 90)
            letter = "A";
        else if (grade >= 80)
            letter = "B";
        else if (grade >= 70)
            letter = "C";
        else if (grade >= 60)
            letter = "D";
        else
            letter = "F";

        // Determine sign (+ or -)
        int lastDigit = grade % 10;
        string sign = "";

        if (letter != "A" && letter != "F") // No A+ or F+/- grades
        {
            if (lastDigit >= 7)
                sign = "+";
            else if (lastDigit < 3)
                sign = "-";
        }
        else if (letter == "A" && lastDigit < 3)
        {
            sign = "-";
        }

        Console.WriteLine($"Your grade is: {letter}{sign}");

        if (grade >= 70)
            Console.WriteLine("Congratulations! You passed the course!");
        else
            Console.WriteLine("Keep trying! You can do better next time.");
    }
}
