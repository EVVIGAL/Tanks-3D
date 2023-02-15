using UnityEngine;

public class WheelRotater : MonoBehaviour
{
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _speed;

    private void Update()
    {
        float rotateSpeed = _speed * _movement.CurrentSpeed * Time.deltaTime;
        if (Mathf.Approximately(rotateSpeed, 0f))
            return;

        foreach (Transform wheel in _wheels)
            wheel.Rotate(wheel.right * rotateSpeed, Space.World);
    }
}