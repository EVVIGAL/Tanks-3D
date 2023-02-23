using UnityEngine;

[RequireComponent(typeof(WeaponReloader), typeof(ProjectilePool))]
public class BotTankWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ParticleSystem _shootFX;

    private WeaponReloader _weaponReloader;
    private ProjectilePool _projectilePool;

    public bool CanShoot => _weaponReloader.CanShoot;

    private void Awake()
    {
        _weaponReloader = GetComponent<WeaponReloader>();
        _projectilePool = GetComponent<ProjectilePool>();
    }

    public void Shoot(Transform target)
    {
        if (_weaponReloader.TryShoot() == false)
            return;

        float barrelAngle = _barrel.transform.localEulerAngles.x;
        barrelAngle = barrelAngle > 180f ? barrelAngle - 360f : barrelAngle;
        barrelAngle *= -1f;
        Vector3 force = GrenadeThrower.CalculatePushForce2(_shootPoint, target.position, barrelAngle);

        Projectile projectile = _projectilePool.Create(_shootPoint, _shootPoint.position, _shootPoint.rotation);
        projectile.Push(force);

        OnShoot();
    }

    protected virtual void OnShoot()
    {
        if (_shootFX != null)
            Instantiate(_shootFX, _shootPoint.position, _shootPoint.rotation);
    }
}