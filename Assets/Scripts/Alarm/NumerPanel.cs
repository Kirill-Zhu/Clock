using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class NumerPanel:MonoBehaviour
{

    private const int numberCount = 2;
    [Inject]
    public Alarma _alram;
    private Queue<int> currentQueue = new Queue<int>();
    public int[] currentArray = new int[2];
    public int[] hoursArray = new int[2];
    public string hoursStirng;
    public TextMeshProUGUI _hoursText;
    public int[] minutesArray = new int[2];
    public string minutesString;
    public TextMeshProUGUI _minutesText;
    //StatePattern
    CurrentNumberPanelState currentState = new CurrentNumberPanelState();
    MinutesPanelState minuteState = new MinutesPanelState();
    HoursPanelState hoursState = new HoursPanelState();

    public void AddNumber(int number)
    {
        if (currentQueue.Count >= numberCount)
            currentQueue.Clear();   
        currentQueue.Enqueue(number);
        currentQueue.CopyTo(currentArray,0);
        currentState.ChangeNumbers(this);
    }
    public void ChooseHoursContainer()
    {        
        currentQueue.Clear();
        currentState = hoursState;

    }
    public void ChooseMinutesContainer()
    {
        currentQueue.Clear();
        currentState = minuteState;
 
    }
    public void WriteAlarmValues()
    {
        _minutesText.text = _alram.minutes.ToString();
        _hoursText.text = _alram.hours.ToString();
    }
}
