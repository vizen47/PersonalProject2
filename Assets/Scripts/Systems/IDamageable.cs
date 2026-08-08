namespace Systems
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void ApplyDamage(int amount);
    }
}