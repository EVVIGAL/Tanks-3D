using UnityEngine;

[RequireComponent (typeof(Movement), typeof(Health), typeof(DefaultWeapon))]
[RequireComponent(typeof(SmokeHealthView), typeof(UIInput), typeof(PlayerInput))]
public class PlayerTank : MonoBehaviour
{
    private Movement _movement;
    private Health _health;
    private DefaultWeapon _weapon;
    private SmokeHealthView _view;
    private PlayerDeathPolicy _playerDeathPolicy;
    private PlayerInput _playerInput;

    public UIInput UIInput { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<DefaultWeapon>();
        _view = GetComponent<SmokeHealthView>();
        UIInput = GetComponent<UIInput>();
        _playerDeathPolicy = GetComponent<PlayerDeathPolicy>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void Init(float moveSpeed, uint maxHealth, uint armor, uint damage, MonoBehaviour healthViewBehaviour, Root root)
    {
        _movement.Init(moveSpeed);
        _health.Init(maxHealth, armor);
        _weapon.Init(damage);
        _view.Init(healthViewBehaviour);
        _playerDeathPolicy.Init(root);
    }

    public void Stop()
    {
        _playerInput.enabled= false;
    }
}