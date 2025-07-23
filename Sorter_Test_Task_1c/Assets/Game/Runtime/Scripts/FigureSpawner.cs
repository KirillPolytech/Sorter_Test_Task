using System;
using System.Collections.Generic;
using Game.Runtime.Scripts.Pools;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Scripts
{
    public class FigureSpawner : MonoBehaviour
    {
        //private Dictionary<string, Type>
        
        private FigurePool _figurePool;
        
        [Inject]
        public void Construct(FigurePool figurePool)
        {
            _figurePool = figurePool;
        }

        public void Begin()
        {
            
            //_figurePool.Get<>()
        }
        
        
    }
}