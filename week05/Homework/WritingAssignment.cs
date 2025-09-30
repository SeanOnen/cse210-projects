public class WritingAssignment : Assignment
{
    private string _title;

    // The WritingAssignment constructor has 3 parameters and
    // passes 2 of them directly to the base constructor.
    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        // Setting variables specific to the WritingAssignment class
        _title = title;
    }

    public string GetWritingInformation()
    {
        // Calling the getter because _studentName is private in the base class
        string studentName = GetStudentName();

        return $"{_title} by {studentName}";
    }
}