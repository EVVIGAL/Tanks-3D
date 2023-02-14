using UnityEngine;

[RequireComponent (typeof(Movement), typeof(Health), typeof(DefaultWeapon))]
[RequireComponent(typeof(SmokeHealthView), typeof(UIInput))]
public class PlayerTank : MonoBehaviour
{
    private Movement _movement;
    private Health _health;
    private DefaultWeapon _weapon;
    private SmokeHealthView _view;

    public UIInput UIInput { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<DefaultWeapon>();
        _view = GetComponent<SmokeHealthView>();
        UIInput = GetComponent<UIInput>();
    }

    public void Init(float moveSpeed, uint maxHealth, uint armor, uint damage, MonoBehaviour healthViewBehaviour)
    {
        _movement.Init(moveSpeed);
        _health.Init(maxHealth, armor);
        _weapon.Init(damage);
        _view.Init(healthViewBehaviour);
    }
}