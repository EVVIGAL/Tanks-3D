using UnityEngine;

public class UIInput : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;
    private IMovement _movement => (IMovement)_movementBehaviour;

    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    [SerializeField] private Barrel _barrel;

    private Vector2 _moveInput;
    private Vector2 _barrelRotateInput;

    public void Shoot()
    {
        if (_weapon.CanShoot)
            _weapon.Shoot(null);
    }

    public void MoveInput(Vector2 delta)
    {
        _moveInput = delta;
    }

    public void BarrelRotate(Vector2 delta)
    {
        _barrelRotateInput = delta;
    }

    private void Update()
    {
        _movement.Move(_moveInput.x);
        _barrel.Rotate(_barrelRotateInput.y);
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