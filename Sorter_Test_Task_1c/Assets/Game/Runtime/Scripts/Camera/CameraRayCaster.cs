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

        private UnityEngine.Camera _camera;
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<PlayerInputSignal>(GetInputData);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<PlayerInputSignal>(GetInputData);
        }

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }
        
        private void GetInputData(PlayerInputSignal inputData)
        {
            if (inputData.InputData.IsHoldingLeftMouseButton)
            {
                CastRay();
            }
        }

        private void CastRay()
        {
            Ray ray = _camera.ScreenPointToRay(_mouse.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit _hit)) 
                return;
            
            _eventBus.Invoke(new ColliderHitSignal(_hit.collider));
        }
    }
}