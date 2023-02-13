using UnityEngine;

[RequireComponent (typeof(Movement), typeof(Health), typeof(DefaultWeapon))]
[RequireComponent(typeof(SmokeHealthView))]
public class PlayerTank : MonoBehaviour
{
    private Movement _movement;
    private Health _health;
    private DefaultWeapon _weapon;
    private SmokeHealthView _view;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<DefaultWeapon>();
        _view = GetComponent<SmokeHealthView>();
    }

    public void Init(float moveSpeed, uint maxHealth, uint armor, uint damage, MonoBehaviour healthViewBehaviour)
    {
        _movement.Init(moveSpeed);
        _health.Init(maxHealth, armor);
        _weapon.Init(damage);
        _view.Init(healthViewBehaviour);
    }
}