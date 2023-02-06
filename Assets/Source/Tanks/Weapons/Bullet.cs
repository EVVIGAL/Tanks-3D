using UnityEngine;

public class Bullet : Projectile
{
    protected override void OnHit(Transform hitTransform, Vector3 position)
    {
        if (hitTransform.TryGetComponent(out IHealth health))
            if (health.IsAlive)
                health.TakeDamage(Damage);

        base.OnHit(hitTransform, position);
    }
}