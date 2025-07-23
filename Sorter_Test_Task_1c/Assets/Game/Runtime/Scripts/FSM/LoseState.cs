using Game.Runtime.Scripts.Windows;

namespace Game.Runtime.Scripts.FSM
{
    public class LoseState : GameState
    {
        private readonly FigureSpawner _figureSpawner;
        private readonly WindowsController _windowsController;

        public LoseState(
            FigureSpawner figureSpawner,
            WindowsController windowsController)
        {
            _figureSpawner = figureSpawner;
            _windowsController = windowsController;
        }

        public override void Enter()
        {
            _figureSpawner.Stop();
            _windowsController.OpenWindow<LoseWindow>();
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