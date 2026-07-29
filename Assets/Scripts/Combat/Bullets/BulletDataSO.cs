using System.Collections.Generic;
using Combat.Bullets;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "SO/Bullet/Data")]
public class BulletDataSO : ScriptableObject
{
    public int damage;
    public int projectileCount;
    public List<BulletEffectSO> effects;
}
