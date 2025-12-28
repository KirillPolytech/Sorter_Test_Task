using Game.Runtime.Scripts.Camera;
using Game.Runtime.Scripts.Factories;
using Game.Runtime.Scripts.Figures;
using Game.Runtime.Scripts.FSM;
using Game.Runtime.Scripts.MVP;
using Game.Runtime.Scripts.Pools;
using Game.Runtime.Scripts.Providers;
using Game.Runtime.Scripts.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Runtime.Scripts.Contexts
{
    public class LevelContext : MonoInstaller
    {
        [SerializeField]
        private CameraRayCaster rayCaster;

        [SerializeField]
        private Figure[] figurePrefabs;

        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text livesText;

        [SerializeField]
        private WindowsController windowsController;

        [FormerlySerializedAs("particleSystem")] [SerializeField]
        private ParticleSystem explosionParticle;

        public override void InstallBindings()
        {
            Container.BindInstance(rayCaster).AsSingle();
            Container.BindInstance(windowsController).AsSingle();

            Container.Bind<VFXProvider>().AsSingle().WithArguments(explosionParticle);
            Container.BindInterfacesAndSelfTo<FigureVFX>().AsSingle();

            Container.BindInterfacesAndSelfTo<FigurePrefabsProvider>().AsSingle().WithArguments(figurePrefabs);
            Container.BindInterfacesAndSelfTo<FigureFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FigurePool>().AsSingle();

            Container.BindInterfacesAndSelfTo<FigureSpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<DragSystem.DragSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
            BindScore();
            BindLives();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }

        private void BindScore()
        {
            Container.BindInterfacesAndSelfTo<ScorePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreView>().AsSingle().WithArguments(scoreText);
        }

        private void BindLives()
        {
            Container.BindInterfacesAndSelfTo<LivesPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LivesView>().AsSingle().WithArguments(livesText);
        }
    }
}