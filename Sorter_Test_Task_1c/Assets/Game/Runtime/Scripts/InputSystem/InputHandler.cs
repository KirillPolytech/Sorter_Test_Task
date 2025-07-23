using Game.Runtime.Scripts.EventBusThings;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Runtime.Scripts.InputSystem
{
    public class InputHandler : ITickable
    {
        private readonly EventBus _eventBus;
        private readonly Mouse _mouse;

        private InputData _inputData;
    

        [Inject]
        public InputHandler(EventBus eventBus)
        {
            _eventBus = eventBus;

            _mouse = Mouse.current;
        }
    
        public void Tick()
        {
            _inputData.IsHoldingLeftMouseButton = _mouse != null && _mouse.leftButton.isPressed;

            _eventBus.Invoke(new PlayerInputSignal(_inputData));
        }
    }
}