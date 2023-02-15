using UnityEngine;

[RequireComponent(typeof(DefaultWeapon))]
public class BotTankWeapon : MonoBehaviour, IWeaponComposite
{
    [SerializeField] private Barrel _barrel;

    private DefaultWeapon _defaultWeapon;

    private void Awake()
    {
        _defaultWeapon = GetComponent<DefaultWeapon>();
    }

    public void Shoot(Transform target)
    {
        _defaultWeapon.Reload();
        float barrelAngle = _barrel.transform.localEulerAngles.x;
        barrelAngle = barrelAngle > 180f ? barrelAngle - 360f : barrelAngle;
        barrelAngle *= -1f;
        Vector3 pushForce = GrenadeThrower.CalculatePushForce2(_defaultWeapon.ShootPoint, target.position, barrelAngle);
        _defaultWeapon.Shoot(null, pushForce);
    }

    public void Reload() { }
}