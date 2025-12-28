using Game.Runtime.Scripts.FSM;
using UnityEngine.UI;
using Zenject;

namespace Game.Runtime.Scripts.Buttons
{
    public class RestartButton : Button
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            onClick.AddListener(_gameStateMachine.Enter<GamePlayState>);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            onClick.RemoveListener(_gameStateMachine.Enter<GamePlayState>);
        }
    }
}
