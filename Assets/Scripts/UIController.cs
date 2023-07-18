using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [Inject]
    private ClockController clockController;
    [Inject]
    public NumerPanel _numerPanel;
    [SerializeField] GameObject _alarmPanel;
    [Inject]
    [SerializeField] UIRoundAlarm _roundAlarm;
    [Inject]
    [SerializeField] Alarma _alarm;
    [SerializeField] Image _alarmStatusImage;
    
    

    [SerializeField] TextMeshProUGUI _textAMorPMt;

    private void Start()
    {
        WrightPMText();
        UpdateAlarmText();
        SetStatusImage();

    }
    public void AlarmSwitcehr()
    {
        if (_alarmPanel.activeInHierarchy)
            AlarmPanelSetInactive();
        else
            AlarmPanelSetActive();
    }
    public void AlarmPanelSetActive()
    {
        _alarmPanel.SetActive(true);

    }
    public void AlarmPanelSetInactive()
    {
        _alarmPanel.SetActive(false);
    }
    public void SetAlarmOn()
    {
        clockController.SetAlarmOn();
        SetStatusImage();
    }
    public void SetAlarmOff()
    {
        clockController.SetAlarmOff();
        SetStatusImage();
    }
    public void SwtichPm()
    {
        _roundAlarm.AMandPMSwitch();
        WrightPMText();
    }
    private void WrightPMText()
    {
        if (_roundAlarm.GetPMBool())
            _textAMorPMt.text = "PM";
        else
            _textAMorPMt.text = "AM";
    }
    private void UpdateAlarmText()
    {
        _numerPanel.WriteAlarmValues();
    }
    private void SetStatusImage()
    {
        if (_alarm.alarmIsOn)
            _alarmStatusImage.sprite = Resources.Load<Sprite>("Icons/AlarmOn");
        else
            _alarmStatusImage.sprite = Resources.Load<Sprite>("Icons/AlarmOff");
    }
}
