using UnityEngine;
using UnityEngine.InputSystem;
using Input = Tank.Input;

public class PlayerInput : MonoBehaviour, ICharacterInputSource
{
    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    private Input _input;

    public Vector2 MovementInput { get; private set; }

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
        MovementInput = _input.Player.Move.ReadValue<Vector2>();
    }

    private void OnPlayerShoot(InputAction.CallbackContext context)
    {
        if (_weapon.CanShoot)
            _weapon.Shoot(null);
    }

    private void OnValidate()
    {
        if (_weaponBehaviour && !(_weaponBehaviour is IWeapon))
        {
            Debug.LogError(nameof(_weaponBehaviour) + " needs to implement " + nameof(IWeapon));
            _weaponBehaviour = null;
        }
    }
}