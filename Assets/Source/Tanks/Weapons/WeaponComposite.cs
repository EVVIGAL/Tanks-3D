using UnityEngine;

[RequireComponent (typeof(DefaultWeapon))]
public class WeaponComposite : MonoBehaviour, IWeaponComposite
{
    private DefaultWeapon _defaultWeapon;

    private void Awake()
    {
        _defaultWeapon = GetComponent<DefaultWeapon>();
    }

    public void Shoot(Transform target)
    {
        _defaultWeapon.Reload();
        _defaultWeapon.Shoot(target);
        OnShoot(target);
    }

    public void Reload()
    {
        OnReload();
    }

    protected virtual void OnShoot(Transform target) { }

    protected virtual void OnReload() { }
}