using Game.Runtime.Scripts.InputSystem;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class PlayerInputSignal
    {
        public InputData InputData;

        public PlayerInputSignal(InputData inputData)
        {
            InputData = inputData;
        }
    }
}