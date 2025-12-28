using Game.Runtime.Scripts.Figures;
using Zenject;

namespace Game.Runtime.Scripts.Providers
{
    public class FigurePrefabsProvider
    {
        public Figure[] FigurePrefabs { get; private set; }

        [Inject]
        public FigurePrefabsProvider(Figure[] figurePrefabs)
        {
            FigurePrefabs = figurePrefabs;
        }
    }
}