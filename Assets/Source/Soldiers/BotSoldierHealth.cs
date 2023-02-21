using UnityEngine;

[RequireComponent (typeof(Ragdoll), typeof(Animator), typeof(Movement))]
public class BotSoldierHealth : BotHealth
{
    [SerializeField] private ObjectPhysic _weaponPhysic;

    private Ragdoll _ragdoll;
    private Animator _animator;
    private Movement _movement;

    private void Start()
    {
        _ragdoll = GetComponent<Ragdoll>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    protected override void Die()
    {
        base.Die();
        _ragdoll.Enable();
        _animator.enabled = false;
        _movement.Disable();

        if (_weaponPhysic != null)
        {
            _weaponPhysic.Enable();
            _weaponPhysic.transform.parent = null;
        }
    }
}