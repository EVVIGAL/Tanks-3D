using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterComposite : WeaponComposite
{
    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    protected override void OnShoot(Transform target)
    {
        _characterAnimator.Shoot();
    }

    protected override void OnReload()
    {
        _characterAnimator.Reload();
    }
}