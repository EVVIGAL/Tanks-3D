using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _radius;
    [SerializeField] private float _castDistance = 1f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _liveTime;
    [SerializeField] private float _pushForce;
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

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hitInfo, _castDistance, _layer))
            if (hitInfo.transform != transform)
                OnHit(hitInfo.transform, hitInfo.point);
    }

    public void Init(uint damage, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        transform.SetPositionAndRotation(position, rotation);
        transform.parent = parent;
        Damage = damage;
        _runningTime = 0f;
        _collider.enabled = true;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        enabled = true;
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }

    public void Disable()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        enabled = false;
    }

    public void Push(float force)
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void Push(Vector3 force)
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
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