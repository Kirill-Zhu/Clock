using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameManager : MonoBehaviour
{
    [Inject]
    [SerializeField] Alarma _alarm;

    private void Start()
    {
        LoadGame(_alarm);
    }
    private void OnApplicationQuit()
    {
        SaveGame(_alarm);
    }
    public void SaveGame(Alarma alarm)
    {
        SaveSystem.SaveGame(alarm);
    }
    public void LoadGame(Alarma alarm)
    {
        SaveData data =  SaveSystem.loadSaveData();
        alarm.hours = data.horus;
        alarm.minutes = data.minutes;
        alarm.alarmIsOn = data.alarmIsOn;
    }
}
