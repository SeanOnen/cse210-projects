using System;

public abstract class Goal
{
    protected string _description;
    protected int _points;

    public string Description => _description;
    public int Points => _points;

    protected Goal(string description, int points)
    {
        _description = description;
        _points = points;
    }

    public abstract bool IsComplete();
    public abstract string GetCompletionString();
    public abstract int RecordEvent();
}