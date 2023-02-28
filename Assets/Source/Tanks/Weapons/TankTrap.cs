using UnityEngine;

public class TankTrap : MonoBehaviour, IHealth
{
    [SerializeField] private ParticleSystem _destroyFX;

    public bool IsAlive { get; private set; } = true;

    public uint TakeDamage(uint damage)
    {
        IsAlive = false;

        if (_destroyFX)
            Instantiate(_destroyFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
        return damage;
    }

    public void Heal(uint health) { }
}