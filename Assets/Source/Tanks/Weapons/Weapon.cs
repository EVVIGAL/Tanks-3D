using UnityEngine;

[RequireComponent (typeof(WeaponReloader), typeof(ProjectilePool))]
public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _force;
    [SerializeField] private ParticleSystem _shootFX;

    protected WeaponReloader WeaponReloader { get; private set; }

    private ProjectilePool _projectilePool;

    public bool CanShoot => WeaponReloader.CanShoot;

    private void Awake()
    {
        WeaponReloader = GetComponent<WeaponReloader>();
        _projectilePool = GetComponent<ProjectilePool>();
    }

    public void Shoot(Transform target = null)
    {
        if (WeaponReloader.TryShoot() == false)
            return;

        Transform shootPoint = GetShootPoint();
        Projectile projectile = _projectilePool.Create(_shootPoint, shootPoint.position, shootPoint.rotation);
        projectile.Push(_force);

        OnShoot();
    }

    protected virtual Transform GetShootPoint()
    {
        return _shootPoint;
    }

    protected virtual void OnShoot()
    {
        if (_shootFX != null)
            Instantiate(_shootFX, _shootPoint.position, _shootPoint.rotation);
    }
}