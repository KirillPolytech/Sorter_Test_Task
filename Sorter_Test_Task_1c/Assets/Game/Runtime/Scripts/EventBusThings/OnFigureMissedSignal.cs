using Game.Runtime.Scripts.Figures;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class OnFigureMissedSignal
    {
        public Figure Figure;

        public OnFigureMissedSignal(Figure figure)
        {
            Figure = figure;
        }
    }
}