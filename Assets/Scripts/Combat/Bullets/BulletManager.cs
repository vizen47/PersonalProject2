using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Combat.Bullets
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance;

        [field: SerializeField] public PoolItemSO CurrentBullet {get; private set;}
        
        private void Awake()
        {
            Instance = this;
        }

        public void SetBullet(PoolItemSO bullet)
        {
            CurrentBullet = bullet;
        }
    }
}
