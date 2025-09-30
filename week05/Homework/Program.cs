using System;

class Program
{
    static void Main(string[] args)
    {
        // Base Assignment object
        Assignment a1 = new Assignment("Sean Onen", "Calculus II");
        Console.WriteLine(a1.GetSummary());

        // Derived class assignments
        MathAssignment a2 = new MathAssignment("Mathey Ayot", "Integration", "2.0", "20-25");
        Console.WriteLine(a2.GetSummary());
        Console.WriteLine(a2.GetHomeworkList());

        WritingAssignment a3 = new WritingAssignment("Lynn Olula", "Bioethics", "Ethical Implications of Genetic Engineering ");
        Console.WriteLine(a3.GetSummary());
        Console.WriteLine(a3.GetWritingInformation());
    }
}