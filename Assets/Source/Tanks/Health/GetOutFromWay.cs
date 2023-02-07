using System.Collections;
using UnityEngine;

public class GetOutFromWay : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minRotateSpeed;
    [SerializeField] private float _maxRotateSpeed;
    [SerializeField] private MonoBehaviour _movementBehaviour;
    private IMovement _movement => (IMovement)_movementBehaviour;

    private float _runningTime;

    public void GetOut()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        _minRotateSpeed = Random.Range(_minRotateSpeed, _maxRotateSpeed);
        int direction = Random.Range(0, 100);
        direction = direction > 50 ? 1 : -1;
        _minRotateSpeed *= direction;

        while (_runningTime <= _moveTime)
        {
            _runningTime += Time.deltaTime;

            transform.Rotate(transform.up * _minRotateSpeed * Time.deltaTime);
            _movement.Move(_moveSpeed);

            yield return null;
        }
    }

    private void OnValidate()
    {
        if (_movementBehaviour && !(_movementBehaviour is IMovement))
        {
            Debug.LogError(nameof(_movementBehaviour) + " needs to implement " + nameof(IMovement));
            _movementBehaviour = null;
        }
    }
}