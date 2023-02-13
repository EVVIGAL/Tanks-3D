using UnityEngine;

[RequireComponent (typeof(Ragdoll), typeof(Animator), typeof(Movement))]
public class SoldierDeathPolicy : EnemyDeathPolicy
{
    [SerializeField] private ObjectPhysic _weaponPhysic;

    private Ragdoll _ragdoll;
    private Animator _animator;
    private Movement _movement;

    private void Awake()
    {
        _ragdoll = GetComponent<Ragdoll>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    protected override void OnDie()
    {
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