using System;
using Game.Runtime.Scripts.EventBusThings;
using Game.Runtime.Scripts.Providers;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Scripts.Figures
{
    public class FigureVFX : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly ParticleSystem _explosion;
        
        [Inject]
        public FigureVFX(EventBus eventBus, VFXProvider vfxProvider)
        {
            _eventBus = eventBus;
            _explosion = vfxProvider.Explosion;
        }

        private void OnFigureMissedSignal(OnFigureMissedSignal onFigureMissedSignal)
        {
            _explosion.gameObject.transform.position = onFigureMissedSignal.Figure.gameObject.transform.position;
            _explosion.Play();
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<OnFigureMissedSignal>(OnFigureMissedSignal);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe<OnFigureMissedSignal>(OnFigureMissedSignal);
        }
    }
}