using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _speed;

    private float _input;

    private void Update()
    {
        _input = Mathf.Clamp(_input, -1f, 1f);
        float speed = Mathf.Approximately(_input, 0f) ? 0 : _speed * _input * -1f * Time.deltaTime;
        float angle = ClampAngle(transform.localEulerAngles.x + speed, _minAngle, _maxAngle);
        var targetAngle = new Vector3(angle, 0f, 0f);
        transform.localEulerAngles = targetAngle;
        _input = 0f;
    }

    public void Rotate(float delta)
    {
        _input += delta;
    }

    private float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }
}