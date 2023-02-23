using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class RPGSoldierWeapon : Weapon
{
    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnShoot()
    {
        base.OnShoot();
        _characterAnimator.Reload();
    }
}