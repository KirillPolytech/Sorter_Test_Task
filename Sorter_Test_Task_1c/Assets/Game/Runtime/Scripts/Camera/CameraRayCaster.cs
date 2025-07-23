using Game.Runtime.Scripts.EventBusThings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Runtime.Scripts.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraRayCaster : MonoBehaviour
    {
        private readonly Mouse _mouse = Mouse.current;

        public UnityEngine.Camera Camera { get; private set; }
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            Camera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                CastRay();
            }
        }

        private void CastRay()
        {
            Vector2 worldPosition = Camera.ScreenToWorldPoint(_mouse.position.ReadValue());

            Collider2D hit = Physics2D.OverlapPoint(worldPosition);
            if (hit == null)
                return;

            _eventBus.Invoke(new ColliderHitSignal(hit));
        }
    }
}