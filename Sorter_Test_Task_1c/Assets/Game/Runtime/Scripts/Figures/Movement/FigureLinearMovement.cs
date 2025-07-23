using Game.Runtime.Scripts.Config;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

namespace Game.Runtime.Scripts.Figures.Movement
{
    public class FigureLinearMovement : IMoveable, ITickable
    {
        private Transform _figureTransform;
        private SplineContainer  _splineContainer;
        private GameConfig _gameConfig;
        
        private float _progressTime;
        private bool _isRunning;
        
        public void Initialize(
            Transform figureTransform, 
            SplineContainer  splineContainer,
            GameConfig gameConfig)
        {
            _figureTransform = figureTransform;
            _splineContainer = splineContainer;
            _gameConfig = gameConfig;
        }

        public void Start()
        {
            _isRunning = true;
            _progressTime = 0;
        }
        
        public void Tick()
        {
            if (!_isRunning)
                return;

            Moving();
        }

        public void Moving()
        {
            _splineContainer.Spline.Evaluate(_progressTime, 
                out var pos, out var tangent, out var up);

            _figureTransform.position = pos;
            
            _progressTime += Time.deltaTime * Random.Range(_gameConfig.FigureSpeedRange[0], _gameConfig.FigureSpeedRange[1]);
        }

        public void Pause()
        {
            _isRunning = false;
        }

        public void Continue()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}