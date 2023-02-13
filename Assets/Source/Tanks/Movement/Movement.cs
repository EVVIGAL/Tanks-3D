using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour, IMovement
{
    [field: SerializeField] public float MaxSpeed;
    [SerializeField] private float _acceleration;

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
        float targetSpeed = Mathf.Approximately(_input, 0f) ? 0 : MaxSpeed * _input;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, Time.deltaTime * _acceleration);
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