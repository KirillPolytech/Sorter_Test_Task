using System.Collections.Generic;
using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.EventBusThings;
using Game.Runtime.Scripts.MVP;
using Game.Runtime.Scripts.Windows;
using Zenject;

namespace Game.Runtime.Scripts.FSM
{
    public class GameStateMachine
    {
        private readonly Dictionary<string, GameState> _states = new();

        public GameState Current { get; private set; }
        
        [Inject]
        public GameStateMachine(
            EventBus signalBus,
            FigureSpawner figureSpawner,
            WindowsController windowsController,
            GameModel gameModel,
            GameConfig gameConfig)
        {
            _states.Add(typeof(WinState).ToString(), new WinState(figureSpawner, windowsController));
            _states.Add(typeof(LoseState).ToString(), new LoseState(figureSpawner, windowsController));
            _states.Add(typeof(GamePlayState).ToString(), 
                new GamePlayState(figureSpawner, windowsController, gameModel, gameConfig));

            Enter<GamePlayState>();
        }

        public void Enter<T>() where T : GameState
        {
            Current?.Exit();
            Current = _states[typeof(T).ToString()];
            Current.Enter();
        }
    }
}