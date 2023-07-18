using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Alarm")]
public class Alarma : ScriptableObject
{
     public bool isRingingNow = false;
     public bool alarmIsOn = false;
     public int hours;
     public int minutes;


}
