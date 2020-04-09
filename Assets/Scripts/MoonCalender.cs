using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoonCalender
{
    /*Okay! So the MoonCalender is a special script.
     * It uses a 5th Dimensional Array to figure out the exact timing
     * in when it's a full moon based on your time zone and time itself.
     First is Mouth, Day, Year, Hour, and Minutes.
     It'll match the exact timing of when a full moon should occur,
     which approximently it is 27 days 7 hours 43 minutes and 11.5 seconds
     for 1 Lunar Orbit.
         */

    struct CleasingPosition
    {
        public int month, day, year, hour, minute;

        public CleasingPosition(int month, int day,int year,int hour,int minute)
        {
            this.month = month;
            this.day = day;
            this.year = year;
            this.hour = hour;
            this.minute = minute;
        }
    }

    //We need to know the time
    static DateTime time = DateTime.Now;
    static DateTime PreviousFullMoon = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2020, 4, 7, 19, 35, 0));
    static CleasingPosition position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute);
    static int DaysPassed = 0;

    const uint EVENING = 18;
    const uint CYCLECOMPLETE = 29;
    const uint MORNING = 4;

    //We need to shift the time based on the timezone. This will be very important
    static readonly TimeZoneInfo timeZone;

    public static IEnumerator Begin()
    {
        while (true)
        {
            time = DateTime.Now;
            position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute);
            DaysPassed = (int)(time.Date - PreviousFullMoon.Date).TotalDays;
            Display();
            CheckIfDayOfCleansing();
            yield return null;
        }
    }

    static void Display()
    {
        Debug.Log(DaysPassed);
    }

    static void CheckIfDayOfCleansing()
    {
        if (DaysPassed % CYCLECOMPLETE == 0 && (time.Hour >= EVENING || time.Hour <= MORNING))
        {
            Debug.Log("Today is the Day of Cleansing!");
        }
    }
}
