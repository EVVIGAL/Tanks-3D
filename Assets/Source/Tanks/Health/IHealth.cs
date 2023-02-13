public interface IHealth
{
    uint TakeDamage(uint damage);
    void Heal(uint health);
    bool IsAlive { get; }
}