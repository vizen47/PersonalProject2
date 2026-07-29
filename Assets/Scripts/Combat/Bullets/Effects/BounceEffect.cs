using Combat.Bullets;
using UnityEngine;

namespace GameModules.Bullets.BulletEffects
{
    [CreateAssetMenu(menuName = "SO/Bullet/Bounce Effect")]
    public class BounceEffect : BulletEffectSO
    {
        public override void OnHit(Bullet bullet)
        {
            // 여기서 벽을 튕기는 기능 구현
        }
    }
}
