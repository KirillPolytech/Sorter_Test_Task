using System;
using System.Collections.Generic;
using Game.Runtime.Scripts.Config;
using Game.Runtime.Scripts.Factories;
using Game.Runtime.Scripts.Figures;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Runtime.Scripts.Pools
{
    public class FigurePool : IInitializable
    {
        private readonly FigureFactory _factory;
        private readonly GameConfig _gameConfig;
        private readonly Dictionary<Type, Queue<Figure>> _pool = new();
        
        public readonly List<Type> _figureTypes = new() 
            {typeof(SquareFigure), typeof(CircleFigure), typeof(StarFigure), typeof(TriangleFigure)};


        [Inject]
        public FigurePool(FigureFactory factory, GameConfig gameConfig)
        {
            _factory = factory;
            _gameConfig = gameConfig;
        }
        
        public void Initialize()
        {
            for (int i = 0; i < _figureTypes.Count; i++)
            {
                for (int j = 0; j < _gameConfig.WinFiguresCount / _figureTypes.Count; j++)
                {
                    Return(_factory.Create(_figureTypes[i]));
                }
            }
        }

        public Figure Get(Type type)
        {
            if (_pool.TryGetValue(type, out var queue) && queue.Count > 0)
            {
                var instance = queue.Dequeue();
                return instance;
            }

            return _factory.Create(type);
        }
        
        public T Get<T>(Type type) where T : Figure
        {
            if (_pool.TryGetValue(type, out var queue) && queue.Count > 0)
            {
                var instance = queue.Dequeue();
                return instance as T;
            }

            return _factory.Create<T>();
        }

        public void Return(Figure figure)
        {
            Type type = figure.GetType();

            if (!_pool.ContainsKey(type))
            {
                _pool.Add(type, new Queue<Figure>());
            }

            figure.gameObject.SetActive(false);
            _pool[type].Enqueue(figure);
        }
    }
}