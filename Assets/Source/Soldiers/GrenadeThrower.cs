using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DefaultWeapon))]
public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] private float _angle;

    private Transform _target;
    private DefaultWeapon _defaultWeapon;

    private void Awake()
    {
        _defaultWeapon = GetComponent<DefaultWeapon>();
    }

    public void Shoot(Transform target)
    {
        _target = target;
    }

    public void TakeUp()
    {
        _defaultWeapon.Reload();
        _defaultWeapon.Projectile.Disable();
    }

    public void Throw()
    {
        _defaultWeapon.Shoot(_target, CalculatePushForce(_defaultWeapon.ShootPoint, _target.position, _angle));
        StartCoroutine(EnableProjectile());
    }

    private IEnumerator EnableProjectile()
    {
        yield return new WaitForSeconds(0.25f);
        _defaultWeapon.Projectile.Enable();
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
}