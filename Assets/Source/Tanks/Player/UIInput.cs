using UnityEngine;

public class UIInput : MonoBehaviour, ICharacterInputSource
{
    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    public Vector2 MovementInput { get; private set; }

    public void MoveForward()
    {
        MovementInput = new Vector2(1f, MovementInput.y);
    }

    public void MoveBackward()
    {
        MovementInput = new Vector2(-1f, MovementInput.y);
    }

    public void BarrelUp()
    {
        MovementInput = new Vector2(MovementInput.x, 1f);
    }

    public void BarrelDown()
    {
        MovementInput = new Vector2(MovementInput.x, -1f);
    }

    public void Stop()
    {
        MovementInput = Vector2.zero;
    }

    public void Shoot()
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