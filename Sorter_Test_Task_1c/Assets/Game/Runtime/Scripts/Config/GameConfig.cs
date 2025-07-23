using UnityEngine;

namespace Game.Runtime.Scripts.Config
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Config/Game Settings")]
    public class GameConfig : ScriptableObject
    {
        public int Lives;
        public int FiguresCount;

        public Vector2 FigureSpeedRange = new (1,5);

        public string LivesText = "Lives:";
        public string ScoreText = "Score:";
    }
}