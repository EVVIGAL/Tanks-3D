using UnityEngine;

public class DefaultWeapon : Weapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletPushForce;
    [SerializeField] private MonoBehaviour _projectileFactoryBehaviour;
    private IProjectileFactory _projectileFactory => (IProjectileFactory)_projectileFactoryBehaviour;

    [SerializeField] private ParticleSystem _shootFX;

    protected override void OnShoot(Transform target = null)
    {
        IProjectile projectile = _projectileFactory.Create();
        if (projectile == null)
            return;

        projectile.Init(Damage, _shootPoint.position, _shootPoint.rotation);
        projectile.Push(_bulletPushForce);

        if (_shootFX != null)
            Instantiate(_shootFX, _shootPoint.position, _shootPoint.rotation);
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