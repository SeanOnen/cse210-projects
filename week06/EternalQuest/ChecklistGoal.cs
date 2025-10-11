public class ChecklistGoal : Goal
{
    private readonly int _target;
    private readonly int _bonus;
    private int _completedCount;

    public int Target => _target;
    public int Bonus => _bonus;
    public int CompletedCount => _completedCount;

    public ChecklistGoal(string description, int points, int target, int bonus, int completedCount = 0) : base(description, points)
    {
        _target = target;
        _bonus = bonus;
        _completedCount = completedCount;
    }

    public override bool IsComplete() => _completedCount >= _target;

    public override string GetCompletionString() => $"Completed {_completedCount}/{_target}";

    public override int RecordEvent()
    {
        _completedCount++;
        int pointsAwarded = Points;
        if (_completedCount >= _target)
        {
            pointsAwarded += _bonus;
        }
        return pointsAwarded;
    }
}