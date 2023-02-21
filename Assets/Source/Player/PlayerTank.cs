using UnityEngine;

[RequireComponent (typeof(Movement), typeof(PlayerHealth), typeof(DefaultWeapon))]
[RequireComponent (typeof(UIInput), typeof(PlayerInput))]
public class PlayerTank : MonoBehaviour
{
    private Movement _movement;
    private PlayerHealth _health;
    private DefaultWeapon _weapon;
    private PlayerInput _playerInput;

    public UIInput UIInput { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<PlayerHealth>();
        _weapon = GetComponent<DefaultWeapon>();
        UIInput = GetComponent<UIInput>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void Init(float moveSpeed, uint maxHealth, uint armor, uint damage, MonoBehaviour healthViewBehaviour, Root root)
    {
        _movement.Init(moveSpeed);
        _health.Init(maxHealth, armor, root, healthViewBehaviour);
        _weapon.Init(damage);
    }

    public void Stop()
    {
        _playerInput.enabled= false;
    }
}