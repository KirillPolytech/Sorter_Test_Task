using System;
using System.Collections.Generic;
using Game.Runtime.Scripts.Factories;
using Game.Runtime.Scripts.Figures;
using Zenject;

namespace Game.Runtime.Scripts.Pools
{
    public class FigurePool
    {
        private readonly FigureFactory _factory;
        private readonly Dictionary<Type, Queue<Figure>> _pool = new();

        [Inject]
        public FigurePool(FigureFactory factory)
        {
            _factory = factory;
        }

        public T Get<T>() where T : Figure
        {
            Type type = typeof(T);

            if (_pool.TryGetValue(type, out var queue) && queue.Count > 0)
            {
                var instance = queue.Dequeue();
                instance.gameObject.SetActive(true);
                return (T) instance;
            }

            return _factory.Create<T>();
        }

        public void Return(Figure figure)
        {
            Type type = figure.GetType();

            if (!_pool.TryGetValue(type, out Queue<Figure> queue))
            {
                queue = new Queue<Figure>();
                _pool[type] = new Queue<Figure>();
            }

            figure.gameObject.SetActive(false);
            queue.Enqueue(figure);
        }
    }
}