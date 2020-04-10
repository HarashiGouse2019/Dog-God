using System;
public struct CleasingPosition
{
    public double month, day, year, hour, minute, second;
    public static CleasingPosition zero = new CleasingPosition(0, 0, 0, 0, 0, 0);
    public double[] date;
    public CleasingPosition(double month, double day, double year, double hour, double minute, double second)
    {
        this.month = month;
        this.day = day;
        this.year = year;
        this.hour = hour;
        this.minute = minute;
        this.second = second;

        date = new double[6]
        {
            month,
            day,
            year,
            hour,
            minute,
            second
        };
    }
}
