using UnityEngine;

public class Fly : MonoBehaviour, IMovement
{
    [SerializeField] private float _speed;

    private float _input;

    public void Move(float delta)
    {
        _input += delta;
    }

    private void Update()
    {
        transform.position += transform.forward * (_speed * _input * Time.deltaTime);
        _input = 0f;
    }
}