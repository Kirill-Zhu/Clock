using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class SceneInstaller : MonoInstaller 
{
    [SerializeField] ClockController _clockController;
    [SerializeField] NumerPanel _numerPanel;
    [SerializeField] UIRoundAlarm _UIRoundAlarm;

    public override void InstallBindings()
    {
        Container.BindInstances(_clockController);
        Container.BindInstance(_numerPanel);
        Container.BindInstances(_UIRoundAlarm);
    }
}
