using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;


public class UIRoundAlarm : MonoBehaviour
{   [Inject]
    [SerializeField] Alarma _alarm;
    [Inject]
    private NumerPanel _numerPanel;
    [SerializeField] GameObject _arrow;
    [SerializeField] Joystick _joystick;
    [SerializeField] bool PM;
    public int hours;
    public int minutes;
    DateTime time = new DateTime();
    //Test
    public float textRotZ;
    
    private void Update()
    {     
        SetRotationByTouch();
    }
   private void SetRotationByTouch()
    {

        if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            return;

        var tmpEuler = _arrow.transform.eulerAngles;
        if (_joystick.Vertical > 0||_joystick.Vertical==0)                   
                if(_joystick.Horizontal<=0)           
                   tmpEuler.z = -_joystick.Horizontal * 90;
                          
                else           
                   tmpEuler.z = 360 -_joystick.Horizontal * 90;
                              
        if (_joystick.Vertical < 0)
        {
            if (_joystick.Horizontal >= 0)
                tmpEuler.z = 270 + (_joystick.Vertical * 90);
            else
                tmpEuler.z = 90 + (-_joystick.Vertical * 90);
        }


        _arrow.transform.eulerAngles = tmpEuler;
        textRotZ = tmpEuler.z;
        UpdateValues((int)tmpEuler.z);
        SetAlarmValues(hours, minutes);
        _numerPanel.WriteAlarmValues();
    }

    public void UpdateValues(int rot)
    {

        int tmpFormula = Mathf.Abs(720 - rot * 2);
        time = new DateTime();
        time = time.AddMinutes(tmpFormula);
        minutes = time.Minute;

        if (PM)
            hours = time.Hour + 12;
        else
            hours = time.Hour;
     
    
    }

    private void SetAlarmValues(int hours, int minutes)
    {
        _alarm.hours = hours;
        _alarm.minutes = minutes;

    }
    [ContextMenu("SwoithPm")]
    public void AMandPMSwitch()
    {
        PM = !PM;
        Debug.Log("Pm is" + PM);      
    }
    public bool GetPMBool()
    {
        return PM;
    }
}
