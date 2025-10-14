using System;

public abstract class Activity
{
    private DateTime _date;
    private double _minutes;

    public DateTime Date 
    { 
        get { return _date; } 
        set { _date = value; } 
    }

    public double Minutes 
    { 
        get { return _minutes; } 
        set { _minutes = value; } 
    }

    public Activity(DateTime date, double minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public abstract double GetDistance();

    public abstract double GetSpeed();

    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min) - Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}