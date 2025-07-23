using Game.Runtime.Scripts.Config;
using InTheLens.Runtime.Quests;
using Zenject;

namespace Game.Runtime.Scripts.MVP
{
    public class GameModel
    {
        public ChangedProperty<int> Score { get; } = new(0);
        public ChangedProperty<int> Lives { get; } = new(3);
        public ChangedProperty<int> RemainingFigures { get; set; } = new(0);

        [Inject]
        public GameModel(GameConfig gameConfig)
        {
            Lives.Value = gameConfig.Lives;
            RemainingFigures.Value = gameConfig.FiguresCount;
        }
    }
}