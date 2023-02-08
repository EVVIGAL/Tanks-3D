using UnityEngine;

[RequireComponent (typeof(Ragdoll), typeof(Animator), typeof(CharacterController))]
public class SoldierDeathPolicy : MonoBehaviour, IDeathPolicy
{
    [SerializeField] private ObjectPhysic _weaponPhysic;
    [SerializeField] private int _deathLayer;

    private Ragdoll _ragdoll;
    private Animator _animator;
    private CharacterController _characterController;

    private void Awake()
    {
        _ragdoll = GetComponent<Ragdoll>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    public void Die()
    {
        _ragdoll.Enable();
        _animator.enabled = false;
        _characterController.detectCollisions = false;
        _weaponPhysic.Enable();
        _weaponPhysic.transform.parent = null;
        gameObject.layer = _deathLayer;
    }
}