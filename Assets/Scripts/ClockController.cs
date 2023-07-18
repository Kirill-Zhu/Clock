using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Zenject;
[RequireComponent(typeof(AudioSource))]
public class ClockController : MonoBehaviour
{
    [SerializeField] int updateRateSec = 3600;
    [Header("Round Clock")]
    [SerializeField] Transform _hour;
    [SerializeField] Transform _minute;
    [SerializeField] Transform _second;
    [Header("Electric Clock")]
    [SerializeField] TextMeshProUGUI _textMeshHour;
    [SerializeField] TextMeshProUGUI _textMesxhMinute;
    [SerializeField] TextMeshProUGUI _textMeshSecond;
    //Alarm
    [Inject]
    [SerializeField] Alarma _alarm;
    [SerializeField] AudioClip _clip;
    private AudioSource _source;
    [Inject]
    [SerializeField] WorldTimeApi _worldTime;
    private DateTime _currentDateTime;

    [ContextMenu("Set Clock arrows")]

    private void Start()
    {
        GetAudioSources();
        StartCoroutine(UpdateAllTime(updateRateSec));
        StartCoroutine(RunTimeArrows());
    }
 
    private IEnumerator RunTimeArrows()
    {
        yield return new WaitForSecondsRealtime(1);

        _currentDateTime = _currentDateTime.AddSeconds(1);
        UpdateArrows();
        UpdateElectrioncClock();
        CheckAlarm();
        Debug.Log(_currentDateTime);
        StartCoroutine(RunTimeArrows());

    }
    public IEnumerator UpdateAllTime(int sec)
    {
        _worldTime.UpdateServerTime();
        _currentDateTime = _worldTime.currentTime;
        UpdateArrows();
        UpdateElectrioncClock();
        yield return new WaitForSecondsRealtime(10);
        StartCoroutine(UpdateAllTime(sec));
    }
    #region UpdateArrows
    private void UpdateArrows()
    {
        UpdateSecondArrow();
        UpdateMinuteArrow();
        UpdateHourArrow();
    }
    private void UpdateSecondArrow()
    {
        _second.transform.localEulerAngles = new Vector3(0, 0, _currentDateTime.Second * -6);
    }
    private void UpdateMinuteArrow()
    {
        _minute.transform.localEulerAngles = new Vector3(0, 0, _currentDateTime.Minute * -6);
    }
    private void UpdateHourArrow()
    {
        _hour.transform.localEulerAngles = new Vector3(0, 0, _currentDateTime.Hour * -30);
    }
    #endregion
    #region UpdateElectonicClock
    private void UpdateElectrioncClock()
    {
        UpdateElectonicHour();
        UpdateElectronicMinute();
        UpdateElectonicSecond();
    }
    private void UpdateElectonicHour()
    {
        _textMeshHour.text = _currentDateTime.Hour.ToString();
    }
    private void UpdateElectronicMinute()
    {
        _textMesxhMinute.text = _currentDateTime.Minute.ToString();
    }
    private void UpdateElectonicSecond()
    {
        _textMeshSecond.text = _currentDateTime.Second.ToString();
    }
    #endregion
   
    #region ALARM
    private void CheckAlarm()
    {
        if (_alarm.alarmIsOn &&!_alarm.isRingingNow &&_currentDateTime.Hour == _alarm.hours && _currentDateTime.Minute == _alarm.minutes)
            ALARM();
    }
   
   
    [ContextMenu("Set Alarm On")]
    public void SetAlarmOn()
    {
        _alarm.isRingingNow = false;
        _alarm.alarmIsOn = true;
    }
    [ContextMenu("Set Alarm Off")]
    public void SetAlarmOff()
    {
        _alarm.alarmIsOn = false;
        _alarm.isRingingNow = false;
        _source.Stop();
    }

    private void GetAudioSources()
    {
        _source = GetComponent<AudioSource>();
        if (_clip == null)
            _clip = Resources.Load<AudioClip>("Alarm");
    }
    private void ALARM()
    {
        if (_alarm.isRingingNow)
            return;

        _alarm.isRingingNow = true;
        Debug.Log("ALARM!!!!");
       
        if(!_source.isPlaying)
            _source.PlayOneShot(_clip);
    }
    #endregion
}
