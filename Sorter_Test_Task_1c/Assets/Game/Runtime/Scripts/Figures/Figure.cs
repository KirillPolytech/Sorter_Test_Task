using System;
using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.DragSystem;
using Game.Runtime.Scripts.EventBusThings;
using Game.Runtime.Scripts.Figures.FigureSlots;
using Game.Runtime.Scripts.Figures.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using Zenject;

namespace Game.Runtime.Scripts.Figures
{
    public abstract class Figure : MonoBehaviour, IDraggable
    {
        public FigureTypes figureType;

        public Transform Transform { get; private set; }
        public bool IsDragging { get; private set; }
        public FigureLinearMovement FigureLinearMovement { get; private set; }
        
        private GameConfig _gameConfig;
        private FigureSlot _figureSlot;
        private EventBus _eventBus;

        [Inject]
        public void Construct(GameConfig gameConfig, EventBus eventBus)
        {
            _gameConfig = gameConfig;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            
            FigureLinearMovement = new 
                FigureLinearMovement(Transform, GetComponent<SplineContainer>(), _gameConfig);
        }

        private void OnEnable()
        {
            FigureLinearMovement.OnFinishedMoving += OnFinishedMoving;
        }

        private void OnDisable()
        {
            FigureLinearMovement.OnFinishedMoving -= OnFinishedMoving;
        }

        private void OnFinishedMoving() => _eventBus.Invoke(new OnFigureMissedSignal(this));

        private void Update()
        {
            FigureLinearMovement.Update();
        }

        public void OnBeginDrag()
        {
            IsDragging = true;
            
            FigureLinearMovement.Stop();
        }

        public void OnDrag(Vector3 pointerWorldPos)
        {
            Transform.position = pointerWorldPos;
        }

        public void OnEndDrag(Vector3 pointerWorldPos)
        {
            if (_figureSlot)
                return;
            
            IsDragging = false;
        }

        public void ReturnTo()
        {
            if (_figureSlot)
                return;
            
            IsDragging = false;
            FigureLinearMovement.Continue();
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            _figureSlot = other?.gameObject.GetComponent<FigureSlot>();
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            _figureSlot = null;
        }
    }
}