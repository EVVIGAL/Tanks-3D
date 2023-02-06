using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [field: SerializeField] public float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    private ICharacterInputSource _characterInput => (ICharacterInputSource)_inputSourceBehaviour;

    private CharacterController _characterController;
    private float _currentSpeed;

    public float Speed => _currentSpeed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float targetSpeed = Mathf.Approximately(_characterInput.MovementInput.x, 0f) ? 0 : _speed * _characterInput.MovementInput.x;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, Time.deltaTime * _acceleration);

        Vector3 velocity = transform.forward * _currentSpeed;
        _characterController.SimpleMove(velocity);
    }

    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }
}