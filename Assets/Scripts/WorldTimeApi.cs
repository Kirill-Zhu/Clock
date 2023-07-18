using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEngine.Networking;
public class WorldTimeApi : MonoBehaviour
{
   
    private struct TimeData {
       public string datetime;
    }
    private struct TimeData2
    {
        public string hours;
        public string minutes;
        public string seconds;
    }

    const string API_URL = "http://worldtimeapi.org/api/ip";
    const string API_URL2 = "https://script.google.com/macros/s/AKfycbyd5AcbAnWi2Yn0xhFRbyzS4qMq1VucMVgVvhul5XqS9HkAyJY/exec";
    [HideInInspector] public DateTime currentTime = DateTime.Now;
    public int hours;
    public int minutes;
    public int seconds;

    private void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPi());     
    }
    public DateTime GetRealTime()
    {
        return currentTime;
    }

    [ContextMenu("Get Time")]
    public void UpdateServerTime()
    {
        StartCoroutine(GetRealDateTimeFromAPi());
    }
    public void SetCLocklTime()
    {
        seconds = GetRealTime().Second;
        minutes = GetRealTime().Minute;
        hours = GetRealTime().Hour;
    }
    /*Json Api
   "abbreviation": "MSK",
 "client_ip": "95.24.28.226",
 "datetime": "2023-07-16T08:11:12.116153+03:00",
 "day_of_week": 0,
 "day_of_year": 197,
 "dst": false,
 "dst_from": null,
 "dst_offset": 0,
 "dst_until": null,
 "raw_offset": 10800,
 "timezone": "Europe/Moscow",
 "unixtime": 1689484272,
 "utc_datetime": "2023-07-16T05:11:12.116153+00:00",
 "utc_offset": "+03:00",
 "week_number": 28
   */
    private IEnumerator GetRealDateTimeFromAPi()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        Debug.Log("Updating Real Time from server 1");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            StartCoroutine(GetRealDateTimeFromAPi2());      
        else
        {
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
            currentTime = ParseDateTime(timeData.datetime);
            Debug.Log("Time Request Complete");
            //string realTime = string.Format("Real time is {0}", currentTime);
            //Debug.Log(realTime);
            SetCLocklTime();
        }      
    }
    // "datetime": "2023-07-16T08:11:12.116153+03:00",
    DateTime ParseDateTime(string datetime)
    {
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

        //match 00:00:00
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;
   

        return DateTime.Parse(string.Format("{0} {1}", date, time));
    }

    /* {"dayofweek":1,"dayofweekName":"Monday",
     * "day":17,
     * "month":7,
     * "monthName":"July",
     * "year":2023,
     * "hours":16,
     * "minutes":48,
     * "seconds":56,
     * "millis":744,
     * "fulldate":"Mon, 17 Jul 2023 16:48:56 +0000",
     * "timezone":"UTC","status":"ok"} */
    IEnumerator GetRealDateTimeFromAPi2 ()
    {
        var webRequest = UnityWebRequest.Get(API_URL2);

        yield return webRequest.SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log("Time2 Request Error");
        else
        {
            TimeData2 timeData2 = JsonUtility.FromJson<TimeData2>(webRequest.downloadHandler.text);
            currentTime = ParseDateTime2(timeData2.hours, timeData2.minutes, timeData2.seconds);
            Debug.Log("Time Request2 Complete");                      
        }

    }
   
    DateTime ParseDateTime2(string hours, string minutes, string seconds)
    {
        int UTCSLag = 2;
        int tmpHours;
        Int32.TryParse(hours, out tmpHours);
        tmpHours += UTCSLag;

        return DateTime.Parse(string.Format("{0}:{1}:{2}", tmpHours, minutes, seconds));
    }
}
