using UnityEngine;

public class WheelRotater : MonoBehaviour
{
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _speed;

    private void Update()
    {
        foreach (Transform wheel in _wheels)
            wheel.Rotate(wheel.up * (_speed * _movement.Speed * Time.deltaTime), Space.World);
    }
}