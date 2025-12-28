using Game.Runtime.Scripts.Config;
using Zenject;

namespace Game.Runtime.Scripts.MVP
{
    public class GameModel : IInitializable
    {
        private readonly GameConfig _gameConfig;

        public ChangedProperty<int> Score { get; } = new(0);
        public ChangedProperty<int> Lives { get; } = new();
        public ChangedProperty<int> RemainingFigures { get; set; } = new(0);
        
        [Inject]
        public GameModel(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void Initialize()
        {
            Lives.Value = _gameConfig.Lives;
            RemainingFigures.Value = _gameConfig.WinFiguresCount;
        }
    }
}