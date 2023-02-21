using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour, IHealth
{
    [SerializeField] private uint _damage;
    [SerializeField] private float _blowRadius;
    [SerializeField] private ParticleSystem _explosionFX;

    public bool IsAlive => true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            return;

        Explose();
    }

    private void Explose()
    {
        var units = new GetObjectsInRadius<IHealth>(_blowRadius, transform);
        IEnumerable healths = units.Get();
        foreach (IHealth health in healths)
            if (health.IsAlive)
                health.TakeDamage(_damage);

        if (_explosionFX)
            Instantiate(_explosionFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _blowRadius);
    }

    public uint TakeDamage(uint damage)
    {
        Explose();
        return _damage;
    }

    public void Heal(uint health) { }
}