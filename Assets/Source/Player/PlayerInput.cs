using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Input = Tank.Input;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _movementBehaviour;
    private IMovement _movement => (IMovement)_movementBehaviour;

    [SerializeField] private MonoBehaviour _weaponBehaviour;
    private IWeapon _weapon => (IWeapon)_weaponBehaviour;

    [SerializeField] private Barrel _barrel;
    [SerializeField] private Skill _skillSlot1;
    [SerializeField] private Skill _skillSlot2;

    private Input _input;
    private bool _isPointerOverGameObject;

    public void Init(Skill skillSlot1, Skill skillSlot2)
    {
        _skillSlot1 = skillSlot1;
        _skillSlot2 = skillSlot2;
    }

    private void OnEnable()
    {
        _input = new Input();
        _input.Enable();
        _input.Player.Shoot.performed += OnPlayerShootPerformed;
        _input.Player.SkillSlot1.performed += OnSkillSlot1Performed;
        _input.Player.SkillSlot2.performed += OnSkillSlot2Performed;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Shoot.performed -= OnPlayerShootPerformed;
        _input.Player.SkillSlot1.performed -= OnSkillSlot1Performed;
        _input.Player.SkillSlot2.performed -= OnSkillSlot2Performed;
    }

    private void Update()
    {
        Vector2 input = _input.Player.Move.ReadValue<Vector2>();
        _movement.Move(input.x);
        _barrel.Rotate(input.y);
        _isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
    }

    private void OnPlayerShootPerformed(InputAction.CallbackContext context)
    {
        if (_isPointerOverGameObject)
            return;

        if (_weapon.CanShoot)
            _weapon.Shoot(null);
    }

    private void OnSkillSlot1Performed(InputAction.CallbackContext context)
    {
        _skillSlot1.Use();
    }

    private void OnSkillSlot2Performed(InputAction.CallbackContext context)
    {
        _skillSlot2.Use();
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