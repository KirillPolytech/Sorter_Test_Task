using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Scripts.Figures;
using Game.Runtime.Scripts.Providers;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Scripts.Factories
{
    public class FigureFactory
    {
        private readonly Dictionary<Type, Figure> _figurePrefabMap;
        private readonly IInstantiator _instantiator;
        private readonly Transform _parent;
        
        [Inject]
        public FigureFactory(FigurePrefabsProvider figurePrefabsProvider, IInstantiator instantiator)
        {
            _figurePrefabMap = figurePrefabsProvider
                .FigurePrefabs
                .ToDictionary(x => x.GetType(), x => x);
            _instantiator = instantiator;
            _parent = new GameObject("FiguresParent").transform;
        }

        public T Create<T>() where T : Figure
        {
            if (_figurePrefabMap.TryGetValue(typeof(T), out Figure figure))
            {
                T t = _instantiator.InstantiatePrefabForComponent<T>(figure);
                t.transform.SetParent(_parent);
                return t;
            }
            
            throw new InvalidOperationException($"[Create] Prefab for type {typeof(T)} not found in factory. [Time:{Time.time}]");
        }
        
        public Figure Create(Type type)
        {
            if (_figurePrefabMap.TryGetValue(type, out Figure figure))
            {
                Figure temp = _instantiator.InstantiatePrefab(figure.gameObject).GetComponent<Figure>();
                temp.transform.SetParent(_parent);
                return temp;
            }
            
            throw new InvalidOperationException($"[Create] Prefab for type {type} not found in factory. [Time:{Time.time}]");
        }
    }
}
