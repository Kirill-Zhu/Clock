using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int horus;
    public int minutes;
    public bool alarmIsOn;
   public SaveData(Alarma alarm)
    {
        horus = alarm.hours;
        minutes = alarm.minutes;
        alarmIsOn = alarm.alarmIsOn;
        
    }
}
