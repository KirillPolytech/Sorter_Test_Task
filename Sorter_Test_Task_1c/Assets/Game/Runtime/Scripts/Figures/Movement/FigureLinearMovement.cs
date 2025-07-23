using System;
using Game.Runtime.Scripts.Config;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Game.Runtime.Scripts.Figures.Movement
{
    public class FigureLinearMovement : IMoveable
    {
        private readonly Transform _figureTransform;
        private readonly SplineContainer  _splineContainer;
        private readonly GameConfig _gameConfig;
        
        public Action OnFinishedMoving;
        
        private float _progressTime;
        private bool _isRunning;
        private float _speed;
        
        public FigureLinearMovement(
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
            Moving();
            _figureTransform.GameObject().SetActive(true);
            
            _isRunning = true;
            _progressTime = 0;
            _speed = Random.Range(_gameConfig.FigureSpeedRange[0], _gameConfig.FigureSpeedRange[1]);
        }
        
        public void Update()
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
            
            _progressTime += Time.deltaTime * _speed;

            if (_progressTime >= 1)
            {
                OnFinishedMoving?.Invoke();
            }
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