using UnityEngine;

namespace Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        protected float LifeTimer;
        protected bool IsDead;
        protected Rigidbody2D Rigid;
        
        protected virtual void Awake()
        {
            Rigid =  GetComponent<Rigidbody2D>();
        }
        
        public abstract void InitAndFire(Transform firePos, int damage, float knockbackPower, float firePower);

        public virtual void ResetItem()
        {
            IsDead = false;
            LifeTimer = 0;
        }
    }
}