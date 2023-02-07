using UnityEngine;

public class UIInput : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;
    private IMovement _movement => (IMovement)_movementBehaviour;

    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    [SerializeField] private Barrel _barrel;

    public void MoveForward()
    {
        _movement.Move(1f);
    }

    public void MoveBackward()
    {
        _movement.Move(-1f);
    }

    public void BarrelUp()
    {
        _barrel.Rotate(1f);
    }

    public void BarrelDown()
    {
        _barrel.Rotate(-1f);
    }

    public void Shoot()
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