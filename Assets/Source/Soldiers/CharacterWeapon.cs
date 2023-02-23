using UnityEngine;

[RequireComponent (typeof(CharacterAnimator))]
public class CharacterWeapon : Weapon
{
    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnShoot()
    {
        _characterAnimator.Shoot();
    }

    public void Reload()
    {
        _characterAnimator.Reload();
    }
}