using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.DragSystem;
using Game.Runtime.Scripts.Figures.Movement;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

namespace Game.Runtime.Scripts.Figures
{
    public abstract class Figure : MonoBehaviour, IDraggable
    {
        public Transform Transform { get; private set; }
        public bool IsDragging { get; private set; }
        public FigureLinearMovement FigureLinearMovement { get; private set; }

        [SerializeField]
        private SplineContainer splineContainer;
        
        private GameConfig _gameConfig;

        [Inject]
        public void Construct(FigureLinearMovement figureLinearMovement, GameConfig gameConfig)
        {
            FigureLinearMovement = figureLinearMovement;
            _gameConfig = gameConfig;
        }

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            
            FigureLinearMovement.Initialize(Transform, splineContainer, _gameConfig);
        }

        public void OnBeginDrag()
        {
            FigureLinearMovement.Stop();
        }

        public void OnDrag(Vector3 pointerWorldPos)
        {
            Transform.position = pointerWorldPos;
        }

        public void OnEndDrag(Vector3 pointerWorldPos)
        {
            
        }

        public void ReturnTo()
        {
            FigureLinearMovement.Continue();
        }
    }
}
