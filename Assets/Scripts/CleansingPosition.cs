using System;
public class CleasingPosition
{
    public double month, day, year, hour, minute, second;
    public TimeZoneInfo timeZone;
    public static CleasingPosition zero = new CleasingPosition(0, 0, 0, 0, 0, 0, TimeZoneInfo.Local);
    public object[] date;
    public CleasingPosition(double month, double day, double year, double hour, double minute, double second, TimeZoneInfo timeZone)
    {
        this.month = month;
        this.day = day;
        this.year = year;
        this.hour = hour;
        this.minute = minute;
        this.second = second;
        this.timeZone = timeZone;

        date = new object[7]
        {
            month,
            day,
            year,
            hour,
            minute,
            second,
            timeZone
        };
    }
}
