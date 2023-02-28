using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _castDistance = 1f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _liveTime;
    [SerializeField] private float _pushForce;
    [SerializeField] private ParticleSystem _hitFX;

    private float _runningTime;
    protected Rigidbody Rigidbody { get; private set; }

    public uint Damage { get; private set; }

    public bool IsActive => gameObject.activeSelf;

    protected bool DetectCollisions;

    public void Init(uint damage)
    {
        Damage = damage;
        _runningTime = 0f;
        DetectCollisions = true;
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _runningTime += Time.deltaTime;
        if (_runningTime >= _liveTime)
        {
            gameObject.SetActive(false);
            return;
        }

        if (DetectCollisions == false)
            return;

        if (Physics.SphereCast(transform.position, _radius, Rigidbody.velocity, out RaycastHit hitInfo, _castDistance, _layer))
            if (hitInfo.transform != transform)
                OnHit(hitInfo);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        enabled = true;
        Rigidbody.isKinematic = false;
    }

    public void Disable()
    {
        Rigidbody.isKinematic = true;
        enabled = false;
    }

    public void Push(float force)
    {
        transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void Push(Vector3 force)
    {
        transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
    }

    protected virtual void OnHit(RaycastHit hitInfo)
    {
        if (hitInfo.collider.isTrigger)
            return;

        if (_hitFX != null)
            Instantiate(_hitFX, hitInfo.point, Quaternion.identity);

        if (hitInfo.transform.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddForce(transform.forward * _pushForce, ForceMode.Impulse);

        gameObject.SetActive(false);
    }
}