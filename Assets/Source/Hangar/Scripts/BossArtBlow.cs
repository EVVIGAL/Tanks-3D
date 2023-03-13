using UnityEngine;

public class BossArtBlow : MonoBehaviour
{
    [SerializeField] private EnemiesCounter _counter;
    [SerializeField] private Quaternion _rotation;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    private float _delay;
    private float _timePassed;

    private void OnEnable()
    {
        _delay = GetRandomDelay();       
    }

    private void Update()
    {
        if (_counter.GetEnemies() <= 0)
            return;

        _timePassed += Time.deltaTime;

        if (_timePassed >= _delay)
        {
            Instantiate(_projectile, transform.position, _rotation);
            _delay = GetRandomDelay();
            _timePassed = 0;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        transform.position = new Vector3(_target.position.x, _target.position.y + 20f, _target.position.z);
        transform.SetParent(_target);
    }

    private float GetRandomDelay()
    {
        float delay = Random.Range(_minDelay, _maxDelay);
        return delay;
    }
}