using System;

public class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, double minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed / 60) * Minutes;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / GetSpeed();
    }
}