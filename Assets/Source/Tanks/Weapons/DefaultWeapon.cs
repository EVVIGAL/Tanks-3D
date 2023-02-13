using UnityEngine;

public class DefaultWeapon : MonoBehaviour
{
    [field: SerializeField] public uint Damage { get; private set; }
    [field: SerializeField] public float BulletPushForce { get; private set; }
    [field: SerializeField] public Transform ShootPoint { get; private set; }

    [SerializeField] private MonoBehaviour _projectileFactoryBehaviour;
    private IProjectileFactory _projectileFactory => (IProjectileFactory)_projectileFactoryBehaviour;

    [SerializeField] private ParticleSystem _shootFX;

    public IProjectile Projectile { get; private set; }

    public void Init(uint damage)
    {
        Damage = damage;
    }

    public void Shoot(Transform target = null, Vector3 force = new Vector3())
    {
        if (force == new Vector3())
            Projectile.Push(BulletPushForce);
        else
            Projectile.Push(force);

        if (_shootFX != null)
            Instantiate(_shootFX, ShootPoint.position, ShootPoint.rotation);
    }

    public void Reload()
    {
        Projectile = _projectileFactory.Create();
        Projectile.Init(Damage, ShootPoint.position, ShootPoint.rotation, ShootPoint);
    }

    private void OnValidate()
    {
        if (_projectileFactoryBehaviour && !(_projectileFactoryBehaviour is IProjectileFactory))
        {
            Debug.LogError(nameof(_projectileFactoryBehaviour) + " needs to implement " + nameof(IProjectileFactory));
            _projectileFactoryBehaviour = null;
        }
    }
}