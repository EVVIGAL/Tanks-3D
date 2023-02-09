using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class GrenadeThrower : Weapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletPushForce;
    [SerializeField] private int _startSpeed;
    [SerializeField] private MonoBehaviour _projectileFactoryBehaviour;
    private IProjectileFactory _projectileFactory => (IProjectileFactory)_projectileFactoryBehaviour;

    private CharacterAnimator _characterAnimator;
    private IProjectile _projectile;
    private Transform _target;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnShoot(Transform target = null)
    {
        _characterAnimator.Shoot();
        _target = target;
    }

    public void TakeUp()
    {
        _projectile = _projectileFactory.Create();
        if (_projectile == null)
            return;

        _projectile.Init(Damage, _shootPoint.position, Quaternion.identity, _shootPoint);
        _projectile.Disable();
    }

    public void Throw()
    {
        if (_projectile == null)
            return;

        _projectile.Push(CalculateTrajectory(transform, _target.position));
        StartCoroutine(EnableProjectile());
    }

    private IEnumerator EnableProjectile()
    {
        yield return new WaitForSeconds(0.25f);
        _projectile.Enable();
    }

    private Vector3 CalculateTrajectory(Transform startPoint, Vector3 target)
    {
        float v2 = _startSpeed * _startSpeed;
        float gravity = Physics.gravity.y;
        float angle;

        float height = startPoint.position.y - target.y;

        float xx = target.x - startPoint.position.x;
        float xz = target.z - startPoint.position.z;
        float x = Mathf.Sqrt(xx * xx + xz * xz);

        float sqrt = (v2 * v2) - (gravity * (gravity * (x * x) + 2 * height * v2));

        if (sqrt < 0)
            return Vector3.zero;

        sqrt = Mathf.Sqrt(sqrt);

        angle = Mathf.Atan((v2 - sqrt) / (gravity * x));

        Quaternion q = Quaternion.Euler(angle * Mathf.Rad2Deg, startPoint.eulerAngles.y, startPoint.eulerAngles.z);

        return (q * Vector3.forward * _startSpeed);
    }
}