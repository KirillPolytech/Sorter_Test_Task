﻿using System;
using Zenject;

namespace Game.Runtime.Scripts.MVP
{
    public class ScorePresenter : IInitializable, IDisposable
    {
        private readonly GameModel _model;
        
        public event Action<int> OnScoreChanged;

        [Inject]
        public ScorePresenter(GameModel model)
        {
            _model = model;
        }

        private void OnChangedHandler()
        {
            OnScoreChanged?.Invoke(_model.Score.Value);
        }

        public void Initialize()
        {
            _model.Score.OnChanged += OnChangedHandler;
        }
        
        public void Dispose()
        {
            _model.Score.OnChanged -= OnChangedHandler;
        }
    }
}