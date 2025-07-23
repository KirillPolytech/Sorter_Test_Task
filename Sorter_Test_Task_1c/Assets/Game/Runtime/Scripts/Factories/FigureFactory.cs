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
        
        [Inject]
        public FigureFactory(FigurePrefabsProvider figurePrefabsProvider, IInstantiator instantiator)
        {
            _figurePrefabMap = figurePrefabsProvider
                .FigurePrefabs
                .ToDictionary(x => x.GetType(), x => x);
            _instantiator = instantiator;
        }

        public T Create<T>() where T : Figure
        {
            if (_figurePrefabMap.TryGetValue(typeof(T), out Figure figure))
            {
                return _instantiator.InstantiatePrefabForComponent<T>(figure);
            }
            
            throw new InvalidOperationException($"[Create] Prefab for type {typeof(T)} not found in factory. [Time:{Time.time}]");
        }
    }
}
