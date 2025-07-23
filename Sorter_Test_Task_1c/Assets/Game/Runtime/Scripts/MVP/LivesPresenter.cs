using System;
using Unity.VisualScripting;

namespace Game.Runtime.Scripts.MVP
{
    public class LivesPresenter : IInitializable, IDisposable
    {
        private readonly GameModel _model;
        
        public event Action<int> OnLivesChanged;

        public LivesPresenter(GameModel model)
        {
            _model = model;
        }
        
        private void OnChangedHandler()
        {
            OnLivesChanged?.Invoke(_model.Score.Value);
        }
        
        public void Initialize()
        {
            _model.Lives.OnChanged += OnChangedHandler;
        }

        public void Dispose()
        {
            _model.Lives.OnChanged -= OnChangedHandler;
        }
    }
}