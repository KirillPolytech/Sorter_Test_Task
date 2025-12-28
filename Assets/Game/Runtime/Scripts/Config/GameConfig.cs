using UnityEngine;

namespace Game.Runtime.Scripts.Config
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Config/Game Settings")]
    public class GameConfig : ScriptableObject
    {
        public int Lives;
        public int WinFiguresCount;

        public Vector2 FigureSpeedRange = new (1,5);
        public Vector2 SpawnDelay = new(0.4f,1f);

        public string LivesText = "Lives:";
        public string ScoreText = "Score:";
    }
}