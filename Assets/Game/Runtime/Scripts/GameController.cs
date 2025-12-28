using System;
using System.Linq;
using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.EventBusThings;
using Game.Runtime.Scripts.FSM;
using Game.Runtime.Scripts.MVP;
using Game.Runtime.Scripts.Pools;
using Zenject;

namespace Game.Runtime.Scripts
{
    public class GameController : IInitializable, IDisposable
    {
        private EventBus _eventBus;
        private GameModel _gameModel;
        private FigureSpawner _figureSpawner;
        private FigurePool _figurePool;
        private GameStateMachine _gameStateMachine;
        private GameConfig _gameConfig;

        [Inject]
        public void Construct(
            EventBus eventBus,
            GameModel gameModel,
            FigureSpawner figureSpawner,
            FigurePool figurePool,
            GameStateMachine gameStateMachine,
            GameConfig gameConfig)
        {
            _eventBus = eventBus;
            _gameModel = gameModel;
            _figureSpawner = figureSpawner;
            _figurePool = figurePool;
            _gameStateMachine = gameStateMachine;
            _gameConfig = gameConfig;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<OnFigureSortedSignal>(OnFigureSorted);
            _eventBus.Subscribe<OnFigureMissedSignal>(OnFigureMissed);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe<OnFigureSortedSignal>(OnFigureSorted);
            _eventBus.UnSubscribe<OnFigureMissedSignal>(OnFigureMissed);
        }

        private void OnFigureSorted(OnFigureSortedSignal signal)
        {
            _figurePool.Return(signal.Figure);
            _gameModel.Score.Value++;
            CheckEndCondition();
        }

        private void OnFigureMissed(OnFigureMissedSignal signal)
        {
            _figurePool.Return(signal.Figure);
            _gameModel.Lives.Value--;
            CheckEndCondition();
        }

        private void CheckEndCondition()
        {
            if (_gameModel.Lives.Value <= 0 
                || (_figureSpawner.Figures.All(x => !x.gameObject.activeSelf) 
                    && _figureSpawner.Figures.Count >= _gameConfig.WinFiguresCount))
            {
                _gameStateMachine.Enter<LoseState>();
                return;
            }

            if (_gameModel.Score.Value >= _gameConfig.WinFiguresCount)
            {
                _gameStateMachine.Enter<WinState>();
            }
        }
    }
}