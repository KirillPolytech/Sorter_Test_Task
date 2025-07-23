using UnityEngine;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class ColliderHitSignal
    {
        public Collider HitObject { get; }

        public ColliderHitSignal(Collider hitObject)
        {
            HitObject = hitObject;
        }
    }
}