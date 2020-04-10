using System;
using System.IO;
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
    static DateTime time = DateTime.Now;
    static TimeZoneInfo currentZone;
    static DateTime PreviousFullMoon;
    static CleasingPosition position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute, time.Second, TimeZoneInfo.Local);
    static double DaysPassed = 0;
    static double DaysTilNextCleansing = 0;

    const uint EVENING = 20;
    const double CYCLECOMPLETEINDAYS = 29.5;
    const uint MORNING = 4;

    static string json;

    public static IEnumerator Begin()
    {
        ReadJson();
        while (true)
        {

            //We got the Universal time, which will be very important.
            //What we need to do is take the Universal time, and convert it to the local time
            //based on the TimeZone.

            PreviousFullMoon = TimeZoneInfo.ConvertTimeToUtc(PreviousFullMoon);

            Debug.Log(DateTime.UtcNow + ", PreviousFullMoon " + PreviousFullMoon);

            time = DateTime.UtcNow;

            //Let's try getting the local time with the UTC time and the PreviousFullMoon

            DateTime diffTime, diffPrevMoon;

            #region TimeZoneTest

            try
            {
                if (TimeZoneInfo.Local.IsDaylightSavingTime(time))
                    currentZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.DaylightName);
                else
                    currentZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.StandardName);

                diffTime = TimeZoneInfo.ConvertTimeFromUtc(time, currentZone);
                diffPrevMoon = TimeZoneInfo.ConvertTimeFromUtc(PreviousFullMoon, currentZone);
                Debug.Log("Time in " + currentZone.DisplayName + " " + diffTime + ", PreviousFullMoon " + diffPrevMoon);
            }
            catch (TimeZoneNotFoundException)
            {
                Debug.LogError("Unable to find the " + currentZone + " zone in the registry.");
            }
            catch (InvalidTimeZoneException)
            {
                Debug.LogError("Registry data on the " + currentZone + " zone has been corrupted.");
            }

            #endregion

            position = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute, time.Second, currentZone);

            DaysPassed = (time - PreviousFullMoon).TotalDays;

            DaysTilNextCleansing = Mathf.RoundToInt((float)(CYCLECOMPLETEINDAYS - DaysPassed));

            CheckIfNightOfCleansing();

            yield return null;
        }
    }

    static void CheckIfNightOfCleansing()
    {
        Debug.Log(DaysTilNextCleansing + " remains until next Cleansing.");
        if (time.Hour >= EVENING || time.Hour <= MORNING)
        {

        }
    }

    static void UpdatePreviousFullMoon(bool firstTime = false)
    {
        if (!firstTime)
        {
            if (DaysPassed % CYCLECOMPLETEINDAYS == 0 && time.Second == 0 && File.Exists(Application.persistentDataPath + "/moonCal.json"))
            {
                CleasingPosition newPosition = new CleasingPosition(time.Month, time.Day, time.Year, time.Hour, time.Minute, time.Second, currentZone);
                json = JsonUtility.ToJson(newPosition);
                File.WriteAllText(Application.persistentDataPath + "/moonCal.json", json);
            }
        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/moonCal.json"))
            {
                CleasingPosition newPosition = new CleasingPosition(PreviousFullMoon.Month, PreviousFullMoon.Day, PreviousFullMoon.Year, PreviousFullMoon.Hour, PreviousFullMoon.Minute, PreviousFullMoon.Second, TimeZoneInfo.Local);
                json = JsonUtility.ToJson(newPosition);
                File.WriteAllText(Application.persistentDataPath + "/moonCal.json", json);
                Debug.Log("Json created....");
            }
            else
            {
                File.Create(Application.persistentDataPath + "/moonCal.json");

                //Recursive function
                UpdatePreviousFullMoon(firstTime);
            }
        }
    }

    static void ReadJson()
    {
        CleasingPosition lastPosition = JsonUtility.FromJson<CleasingPosition>(GetJSONString());
        PreviousFullMoon = new DateTime((int)lastPosition.year, (int)lastPosition.month, (int)lastPosition.day, (int)lastPosition.hour, (int)lastPosition.minute, (int)lastPosition.second);
    }

    public static string GetJSONString() => File.ReadAllText(Application.persistentDataPath + "/moonCal.json");
}
