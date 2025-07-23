namespace Game.Runtime.Scripts.Figures.Movement
{
    public interface IMoveable
    {
        public void Start();
        public void Continue();
        public void Moving();
        public void Pause();
        public void Stop();
    }
}