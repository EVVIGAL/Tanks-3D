using UnityEngine;

[RequireComponent (typeof(CharacterAnimator))]
public class CharacterWeapon : MonoBehaviour, IWeaponComposite
{
    private CharacterAnimator _characterAnimator;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void Shoot(Transform target)
    {
        _characterAnimator.Shoot();
    }

    public void Reload()
    {
        _characterAnimator.Reload();
    }
}