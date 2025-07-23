using System;
using Game.Runtime.Scripts.EventBusThings;
using Zenject;

namespace Game.Runtime.Scripts.DragSystem
{
    public class DragSystem : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        private IDraggable _lastDraggable;
        
        [Inject]
        public DragSystem(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleCollision(ColliderHitSignal colliderHitSignal)
        {
            if (!colliderHitSignal.HitObject.TryGetComponent(out IDraggable draggable))
                return;
            
            if (_lastDraggable != draggable)
            {
                _lastDraggable.ReturnTo();
                _lastDraggable = draggable;
                
                _lastDraggable.OnBeginDrag();
            }
            
            draggable.OnDrag(colliderHitSignal.HitObject.transform.position);
        }

        public void Initialize()
        {
            _eventBus.Subscribe<ColliderHitSignal>(HandleCollision);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe<ColliderHitSignal>(HandleCollision);
        }
    }
}