using UnityEngine;
using UnityEngine.InputSystem;
using Input = Tank.Input;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;
    private IMovement _movement => (IMovement)_movementBehaviour;

    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    [SerializeField] private Barrel _barrel;

    private Input _input;

    private void OnEnable()
    {
        _input = new Input();
        _input.Enable();
        _input.Player.Shoot.performed += OnPlayerShoot;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Shoot.performed -= OnPlayerShoot;
    }

    private void Update()
    {
        Vector2 input = _input.Player.Move.ReadValue<Vector2>();
        _movement.Move(input.x);
        _barrel.Rotate(input.y);
    }

    private void OnPlayerShoot(InputAction.CallbackContext context)
    {
        if (_weapon.CanShoot)
            _weapon.Shoot(null);
    }

    private void OnValidate()
    {
        if (_movementBehaviour && !(_movementBehaviour is IMovement))
        {
            Debug.LogError(nameof(_movementBehaviour) + " needs to implement " + nameof(IMovement));
            _movementBehaviour = null;
        }

        if (_weaponBehaviour && !(_weaponBehaviour is IWeapon))
        {
            Debug.LogError(nameof(_weaponBehaviour) + " needs to implement " + nameof(IWeapon));
            _weaponBehaviour = null;
        }
    }
}