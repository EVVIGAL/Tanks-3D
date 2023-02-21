using System.Collections;
using UnityEngine;

public class Explosives : MonoBehaviour, IHealth
{
    [SerializeField] private uint _damage;
    [SerializeField] private float _blowRadius;
    [SerializeField] private ParticleSystem _explosionFX;

    public bool IsAlive { get; private set; } = true;

    protected void Explose()
    {
        IsAlive = false;
        var units = new GetObjectsInRadius<IHealth>(_blowRadius, transform);
        IEnumerable healths = units.Get();
        foreach (IHealth health in healths)
            if (health.IsAlive)
                health.TakeDamage(_damage);

        if (_explosionFX)
            Instantiate(_explosionFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void Heal(uint health) { }

    public uint TakeDamage(uint damage)
    {
        Explose();
        return _damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _blowRadius);
    }
}