using Game.Runtime.Scripts.EventBusThings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using EventBus = Game.Runtime.Scripts.EventBusThings.EventBus;

namespace Game.Runtime.Scripts.Figures.FigureSlots
{
    public abstract class FigureSlot : MonoBehaviour
    {
        [SerializeField]
        private FigureTypes figureType;

        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Figure figure))
                return;

            if (!figure.IsDragging || Mouse.current.leftButton.isPressed)
                return;
            
            if (figure.figureType == figureType)
                _eventBus.Invoke(new OnFigureSortedSignal(figure));
            else
                _eventBus.Invoke(new OnFigureMissedSignal(figure));
        }
    }
}