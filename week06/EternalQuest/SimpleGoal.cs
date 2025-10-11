public class SimpleGoal : Goal
{
    private bool _completed;

    public SimpleGoal(string description, int points, bool completed = false) : base(description, points)
    {
        _completed = completed;
    }

    public override bool IsComplete() => _completed;

    public override string GetCompletionString() => _completed ? "[X]" : "[ ]";

    public override int RecordEvent()
    {
        if (!_completed)
        {
            _completed = true;
            return Points;
        }
        return 0;
    }
}