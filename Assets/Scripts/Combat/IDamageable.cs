using CoreLib;

namespace Combat
{
    public interface IDamageable
    {
        NotifyValue<int> CurrentHealth { get; set; }
        int MaxHealth { get; }
        void ApplyDamage(int amount);
    }
}