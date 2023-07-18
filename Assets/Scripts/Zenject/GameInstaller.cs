using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] WorldTimeApi _worldTimeApi;

    public override void InstallBindings()
    {
      
        Container.Bind<UIController>().AsSingle();
        Container.BindInstance(_worldTimeApi);
    }
}