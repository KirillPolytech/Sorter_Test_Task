using UnityEngine;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class ColliderHitSignal
    {
        public Collider2D HitObject { get; }

        public ColliderHitSignal(Collider2D hitObject)
        {
            HitObject = hitObject;
        }
    }
}