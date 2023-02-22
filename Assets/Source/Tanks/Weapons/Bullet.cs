using UnityEngine;
using UnityEngine.Events;

public class Bullet : Projectile
{
    [SerializeField] private DamageView _damageViewTemplate;
    [SerializeField] private float _ricochetAngle;
    [SerializeField] private PhysicMaterial _ricochetMaterial;
    [SerializeField] private UnityEvent _onRicocheted;

    protected override void OnHit(RaycastHit hitInfo)
    {
        Vector3 direction = transform.position - hitInfo.point;
        float hitAngle = Vector3.Angle(direction, hitInfo.normal);

        if (hitAngle > _ricochetAngle && hitInfo.collider.sharedMaterial == _ricochetMaterial)
        {
            Vector3 invertDirection = hitInfo.point - transform.position;
            Vector3 ricochetDirection = Vector3.Reflect(invertDirection, hitInfo.normal);
            ricochetDirection.Normalize();
            Rigidbody.velocity = ricochetDirection * Rigidbody.velocity.magnitude;
            DetectCollisions = false;
            _onRicocheted?.Invoke();
            return;
        }

        if (hitInfo.transform.TryGetComponent(out IHealth health))
        {
            if (health.IsAlive)
            {
                uint totalDamage = health.TakeDamage(Damage);
                DamageView damageView = Instantiate(_damageViewTemplate, hitInfo.point, Quaternion.identity);
                damageView.Show(totalDamage);
            }
        }

        base.OnHit(hitInfo);
    }
}