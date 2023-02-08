using UnityEngine;

[RequireComponent (typeof(CharacterAnimator))]
public class CharacterWeapon : TankWeapon
{
    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnReload()
    {
        base.OnReload();
        _characterAnimator.Reload();
    }

    protected override void OnShoot(Transform target = null)
    {
        base.OnShoot(target);
        _characterAnimator.Shoot();
    }
}