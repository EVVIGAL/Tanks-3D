using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class GrenadeThrower : Weapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletPushForce;
    [SerializeField] private MonoBehaviour _projectileFactoryBehaviour;
    private IProjectileFactory _projectileFactory => (IProjectileFactory)_projectileFactoryBehaviour;

    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnShoot(Transform target = null)
    {
        _characterAnimator.Shoot();
    }

    public void Throw()
    {
        IProjectile projectile = _projectileFactory.Create();
        if (projectile == null)
            return;

        projectile.Init(Damage, _shootPoint.position, _shootPoint.rotation);
        projectile.Push(_bulletPushForce);
    }
}