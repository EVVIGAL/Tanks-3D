using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class TurretExplosion : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;
    [SerializeField] private float _upwardsModifier = 1f;
    [SerializeField] private ParticleSystem _explosionFX;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Explose()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        Vector2 randomOffset = Random.insideUnitCircle.normalized;
        Vector3 explosionPosition = transform.position + new Vector3(randomOffset.x, -1f, randomOffset.y) * _radius / 2f;
        _rigidbody.AddExplosionForce(_force, explosionPosition, _radius, _upwardsModifier, ForceMode.Impulse);

        if (_explosionFX != null)
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
    }
}