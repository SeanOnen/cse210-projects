using System;

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, double minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double meters = _laps * 50;
        double miles = meters / 1609.34;
        return miles;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}