using System;
using System.Collections;
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

    //We need to know the time
    static DateTime time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
    static DateTime PreviousFullMoon = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2020, 4, 7, 19, 35, 0));
    static CleasingPosition position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute, time.Second);
    static double DaysPassed = 0;
    static double DaysTilNextCleansing = 0;

    const uint EVENING = 18;
    const double CYCLECOMPLETEINDAYS = 29;
    const uint MORNING = 4;

    public static IEnumerator Begin()
    {
        while (true)
        {

            time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute, time.Second);
            DaysPassed = (time.Date - PreviousFullMoon.Date).TotalDays;
            DaysTilNextCleansing = CYCLECOMPLETEINDAYS - DaysPassed;
            CheckIfDayOfCleansing();
            yield return null;
        }
    }

    static void CheckIfDayOfCleansing()
    {
        Debug.Log(DaysTilNextCleansing + " remains until next Cleansing.");
        if (DaysPassed % CYCLECOMPLETEINDAYS == 0 && (time.Hour >= EVENING || time.Hour <= MORNING))
        {
            Debug.Log("Today is the Day of Cleansing!");
        }
    }
}
