using Game.Runtime.Scripts.Factories;
using Game.Runtime.Scripts.Figures;
using Game.Runtime.Scripts.Figures.Movement;
using Game.Runtime.Scripts.MVP;
using Game.Runtime.Scripts.Pools;
using Game.Runtime.Scripts.Providers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Scripts.Contexts
{
    public class LevelContext : MonoInstaller
    {
        [SerializeField]
        private Figure[] figurePrefabs;

        [SerializeField]
        private TMP_Text scoreText;
        
        [SerializeField]
        private TMP_Text livesText;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FigurePrefabsProvider>().AsSingle().WithArguments(figurePrefabs);
            Container.BindInterfacesAndSelfTo<FigureFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FigurePool>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FigureLinearMovement>().AsTransient();

            Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
            BindScore();
            BindLives();
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