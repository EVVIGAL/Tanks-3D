using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Torque : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.AddTorque(_direction * _speed * Time.deltaTime, ForceMode.Force);
    }
}