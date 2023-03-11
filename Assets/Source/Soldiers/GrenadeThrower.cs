using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WeaponReloader), typeof(ProjectilePool), typeof(CharacterAnimator))]
public class GrenadeThrower : MonoBehaviour, IWeapon
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _activateDelay = 0.3f;

    private CharacterAnimator _characterAnimator;
    private WeaponReloader _weaponReloader;
    private ProjectilePool _projectilePool;
    private Transform _target;
    private Projectile _projectile;
    private Vector3 _targetPosition;

    public bool CanShoot => _weaponReloader.CanShoot;

    private void Awake()
    {
        _weaponReloader = GetComponent<WeaponReloader>();
        _projectilePool = GetComponent<ProjectilePool>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void Shoot(Transform target)
    {
        if (_weaponReloader.TryShoot() == false)
            return;

        _characterAnimator.Shoot();
        _target = target;
        _targetPosition = _target.position;
    }

    public void TakeUp()
    {
        _projectile = _projectilePool.Create(_shootPoint, _shootPoint.position, _shootPoint.rotation);
        _projectile.DisablePhysic();
    }

    public void Throw()
    {
        if (_target)
            _targetPosition = _target.position;

        Vector3 force = CalculatePushForce(_shootPoint, _targetPosition, _angle);
        _projectile.Push(force);
        StartCoroutine(EnableProjectile());
    }

    private IEnumerator EnableProjectile()
    {
        yield return new WaitForSeconds(_activateDelay);
        _projectile.EnablePhysic();
    }

    public static Vector3 CalculatePushForce(Transform startPoint, Vector3 target, float angle)
    {
        Vector3 fromTo = target - startPoint.position;
        var fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
        float x = fromToXZ.magnitude;
        float y = fromTo.y;
        startPoint.LookAt(fromToXZ);
        startPoint.Rotate(-angle, 0f, 0f);

        float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angle * Mathf.Deg2Rad) * x) * Mathf.Pow(Mathf.Cos(angle * Mathf.Deg2Rad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        return startPoint.forward * v;
    }

    public static Vector3 CalculatePushForce2(Transform startPoint, Vector3 target, float angle)
    {
        Vector3 fromTo = target - startPoint.position;
        var fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angle * Mathf.Deg2Rad) * x) * Mathf.Pow(Mathf.Cos(angle * Mathf.Deg2Rad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        return startPoint.forward * v;
    }
}