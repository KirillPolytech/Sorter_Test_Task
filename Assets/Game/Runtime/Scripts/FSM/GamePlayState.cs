using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.MVP;
using Game.Runtime.Scripts.Windows;

namespace Game.Runtime.Scripts.FSM
{
    public class GamePlayState : GameState
    {
        private readonly FigureSpawner _figureSpawner;
        private readonly WindowsController _windowsController;
        private readonly GameModel _gameModel;
        private readonly GameConfig _gameConfig;

        public GamePlayState(
            FigureSpawner figureSpawner,
            WindowsController windowsController,
            GameModel gameModel,
            GameConfig gameConfig)
        {
            _figureSpawner = figureSpawner;
            _windowsController = windowsController;
            _gameModel = gameModel;
            _gameConfig = gameConfig;
        }

        public override void Enter()
        {
            _gameModel.Lives.Value = _gameConfig.Lives;
            _gameModel.Score.Value = 0;
            _windowsController.OpenWindow<MainWindow>();
            _figureSpawner.Begin();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
        }
    }
}