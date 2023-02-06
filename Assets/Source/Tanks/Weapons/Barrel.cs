using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _speed;

    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    private ICharacterInputSource _characterInput => (ICharacterInputSource)_inputSourceBehaviour;

    private void Update()
    {
        float speed = Mathf.Approximately(_characterInput.MovementInput.y, 0f) ? 0 : _speed * _characterInput.MovementInput.y * -1f * Time.deltaTime;
        float angle = ClampAngle(transform.localEulerAngles.x + speed, _minAngle, _maxAngle);
        var targetAngle = new Vector3(angle, 0f, 0f);
        transform.localEulerAngles = targetAngle;        
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

    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }
}