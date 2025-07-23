using UnityEngine;
using Zenject;
using EventBus = Game.Runtime.Scripts.EventBusThings.EventBus;

namespace Game.Runtime.Scripts.Figures.FigureSlots
{
    public abstract class FigureSlot : MonoBehaviour
    {
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
            
            if (figure.IsDragging)
                return;
            
            //_eventBus.Invoke();
        }
    }
}