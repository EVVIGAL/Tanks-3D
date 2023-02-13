using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private DamageView _damageViewTemplate;

    protected override void OnHit(Transform hitTransform, Vector3 position)
    {
        if (hitTransform.TryGetComponent(out IHealth health))
        {
            if (health.IsAlive)
            {
                uint totalDamage = health.TakeDamage(Damage);
                DamageView damageView = Instantiate(_damageViewTemplate, position, Quaternion.identity);
                damageView.Show(totalDamage);
            }
        }

        base.OnHit(hitTransform, position);
    }
}