using System.Collections;
using UnityEngine;

public class AreaDamage : MonoBehaviour, IDamage
{
    [SerializeField] private float _blowRadius;

    public void TakeDamage(RaycastHit hitInfo, uint damage)
    {
        var units = new GetObjectsInRadius<IHealth>(_blowRadius, transform);
        IEnumerable healths = units.Get();
        foreach (IHealth health in healths)
            if (health.IsAlive)
                health.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _blowRadius);
    }
}