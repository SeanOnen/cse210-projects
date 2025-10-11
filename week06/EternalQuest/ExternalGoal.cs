public class EternalGoal : Goal
{
    public EternalGoal(string description, int points) : base(description, points) { }

    public override bool IsComplete() => false;

    public override string GetCompletionString() => "[ ]";

    public override int RecordEvent() => Points;
}