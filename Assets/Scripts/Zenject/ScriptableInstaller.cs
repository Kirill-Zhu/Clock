using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[CreateAssetMenu(menuName = "Custom/ScriptableInstaller")]
public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
{
    [SerializeField] Alarma _alarmla;
    public override void InstallBindings()
    {
        Container.BindInstances(_alarmla);
    }
}
