using System.Collections;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private float _blowRadius;

    protected override void OnHit(Transform hitTransform, Vector3 hitPosition)
    {
        base.OnHit(hitTransform, hitPosition);

        var units = new GetObjectsInRadius<IHealth>(_blowRadius, transform);
        IEnumerable healths = units.Get();
        foreach (IHealth health in healths)
            if (health.IsAlive)
                health.TakeDamage(Damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _blowRadius);
    }
}