using System;
using System.Collections.Generic;
using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.Figures;
using Game.Runtime.Scripts.Pools;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Runtime.Scripts
{
    public class FigureSpawner : ITickable
    {
        private readonly List<Figure> _figures = new();
        
        public IReadOnlyCollection<Figure> Figures => _figures.AsReadOnly();

        private FigurePool _figurePool;
        private GameConfig _gameConfig;
        private float _timer;
        private bool _isRunning;

        [Inject]
        public void Construct(
            FigurePool figurePool,
            GameConfig gameConfig)
        {
            _figurePool = figurePool;
            _gameConfig = gameConfig;
        }

        public void Begin()
        {
            _isRunning = true;
            _timer = 0;

            ClearList();
        }

        public void Stop()
        {
            _isRunning = false;
            _timer = 0;

            ClearList();
        }

        public void Tick()
        {
            if (!_isRunning || _figures.Count >= _gameConfig.WinFiguresCount)
                return;

            if (_timer > Random.Range(_gameConfig.SpawnDelay[0], _gameConfig.SpawnDelay[1]))
            {
                Spawn();
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
        }

        private void Spawn()
        {
            int randNum = Random.Range(0, _figurePool._figureTypes.Count);
            Type randType = _figurePool._figureTypes[randNum];

            Figure figure = _figurePool.Get(randType);
            figure.FigureLinearMovement.Start();

            _figures.Add(figure);
        }

        private void ClearList()
        {
            foreach (var figure in _figures)
                _figurePool.Return(figure);

            _figures.Clear();
        }
    }
}