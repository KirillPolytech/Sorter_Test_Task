using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.EventBusThings;
using Game.Runtime.Scripts.InputSystem;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Scripts.Contexts
{
    public class BootContext : MonoInstaller
    {
        [SerializeField]
        private GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gameConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        }
    }
}