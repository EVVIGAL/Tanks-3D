using UnityEngine;

[RequireComponent (typeof(GrenadeThrower), typeof(CharacterAnimator))]
public class GrenadeThrowerComposite : MonoBehaviour, IWeaponComposite
{
    private GrenadeThrower _grenadeThrower;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _grenadeThrower = GetComponent<GrenadeThrower>();
    }

    public void Shoot(Transform target)
    {
        _grenadeThrower.Shoot(target);
        _characterAnimator.Shoot();
    }

    public void Reload() { }
}