using Game.Runtime.Scripts.Figures;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class OnFigureSortedSignal
    {
        public Figure Figure;

        public OnFigureSortedSignal(Figure figure)
        {
            Figure = figure;
        }
    }
}
