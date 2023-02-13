public interface IHealth
{
    void TakeDamage(uint damage);
    void Heal(uint health);
    bool IsAlive { get; }
}