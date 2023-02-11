using UnityEngine;

public interface IWeaponComposite
{
    void Shoot(Transform target);
    void Reload();
}