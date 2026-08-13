using CoreLib;
using Systems.Pooling;
using UnityEngine;

namespace Combat.Bullets
{
    public class BulletManager : MonoSingleton<BulletManager>
    {
        [field: SerializeField] public PoolItemSO CurrentBullet {get; private set;}

        public void SetBullet(PoolItemSO bullet)
        {
            CurrentBullet = bullet;
        }
    }
}
