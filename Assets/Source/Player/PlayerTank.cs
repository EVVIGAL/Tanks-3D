using UnityEngine;

[RequireComponent (typeof(Movement), typeof(PlayerHealth), typeof(ProjectilePool))]
[RequireComponent (typeof(UIInput), typeof(PlayerInput))]
public class PlayerTank : MonoBehaviour
{
    private Movement _movement;
    private PlayerHealth _health;
    private ProjectilePool _projectile;
    private PlayerInput _playerInput;

    public UIInput UIInput { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<PlayerHealth>();
        _projectile = GetComponent<ProjectilePool>();
        UIInput = GetComponent<UIInput>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void Init(float moveSpeed, uint maxHealth, uint armor, uint damage, MonoBehaviour healthViewBehaviour, Root root, Skill skillSlot1, Skill skillSlot2)
    {
        _movement.Init(moveSpeed);
        _health.Init(maxHealth, armor, root, healthViewBehaviour);
        _projectile.Init(damage);
        _playerInput.Init(skillSlot1, skillSlot2);
    }

    public void Stop()
    {
        _playerInput.enabled = false;
    }
}