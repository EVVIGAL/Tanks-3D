using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour, IMovement
{
    [field: SerializeField] public float MaxSpeed = 10f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _braking = 15f;

    private CharacterController _characterController;
    private float _currentSpeed;
    private float _input;

    public float CurrentSpeed => _currentSpeed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(float maxSpeed)
    {
        MaxSpeed = maxSpeed;
    }

    public void Move(float delta)
    {
        _input += delta;
    }

    private void Update()
    {
        _input = Mathf.Clamp(_input, -1f, 1f);
        float targetSpeed = MaxSpeed * _input;
        float targetAcceleration = Mathf.Approximately(_input, 0f) || (_input > 0f && _currentSpeed < 0f) || (_input < 0f && _currentSpeed > 0f) ? _braking : _acceleration;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, Time.deltaTime * targetAcceleration);
        Vector3 velocity = transform.forward * _currentSpeed;
        _characterController.SimpleMove(velocity);
        _input = 0f;
    }

    public void Disable()
    {
        _characterController.enabled = false;
        enabled = false;
    }
}