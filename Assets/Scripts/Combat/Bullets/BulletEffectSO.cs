using UnityEngine;

namespace Combat.Bullets
{
    public abstract class BulletEffectSO : ScriptableObject
    {
        public virtual void OnFire(Bullet bullet) { }
        public virtual void OnHit(Bullet bullet) { }
    }
}