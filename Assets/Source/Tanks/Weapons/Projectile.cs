using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _raycastDistance = 1f;
    [SerializeField] private float _liveTime;
    [SerializeField] private ParticleSystem _hitFX;

    private float _runningTime;
    private Rigidbody _rigidbody;
    private Collider _collider;

    public uint Damage { get; private set; }

    public bool IsActive => gameObject.activeSelf;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _runningTime += Time.deltaTime;
        if (_runningTime >= _liveTime)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _raycastDistance))
            OnHit(hitInfo.transform, hitInfo.point);
    }

    public void Init(uint damage, Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        Damage = damage;
        _runningTime = 0f;
        _collider.enabled = true;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Push(float force)
    {
        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        _collider.enabled = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.transform, collision.contacts[0].point);
    }

    protected virtual void OnHit(Transform hitTransform, Vector3 position)
    {
        if (_hitFX != null)
            Instantiate(_hitFX, position, Quaternion.identity);

        if (hitTransform.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddForce(transform.forward * _pushForce, ForceMode.Impulse);

        gameObject.SetActive(false);
    }
}