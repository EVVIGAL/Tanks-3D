using UnityEngine;

public class Damageable : Health
{
    [SerializeField] private ParticleSystem _destroyVfx;

    protected override void Die()
    {
        Instantiate(_destroyVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}