using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private uint _damage;
    [SerializeField] private float _liveTime;
    [SerializeField] private float _pushForce;
    [SerializeField] private Transform _mesh;
    [SerializeField] private ParticleSystem _trailVfx;
    [SerializeField] private MonoBehaviour _damageBehaviour;
    private IDamage _damagePolicy => (IDamage)_damageBehaviour;

    [Header("Collision")]
    [SerializeField] private float _castRadius;
    [SerializeField] private LayerMask _hittableLayers = -1;
    [SerializeField] private Transform _tip;

    [Header("Ricochet")]
    [Range (0f, 90f)]
    [SerializeField] private float _ricochetAngle;
    [SerializeField] private PhysicMaterial _ricochetMaterial;
    [SerializeField] private AudioClip _ricochetSound;

    [Header("Impact Vfx")]
    [SerializeField] private ParticleSystem _explosionVfx;
    [SerializeField] private ParticleSystem _dirtVfx;
    [SerializeField] private PhysicMaterial _dirt;

    private bool _detectCollisions;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Vector3 _lastPosition;

    public void Init(uint damage, bool detectCollision = true)
    {
        _damage = damage;
        _detectCollisions = detectCollision;
        StartCoroutine(Disable());

        if (_trailVfx != null)
            _trailVfx.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _detectCollisions = true;
        _lastPosition = transform.position;
    }

    private void Update()
    {
        if (_mesh && _rigidbody.velocity != Vector3.zero)
            _mesh.rotation = Quaternion.LookRotation(_rigidbody.velocity);

        if (_detectCollisions == false)
            return;

        float castDistance = Vector3.Distance(_tip.position, _lastPosition);
        if (Physics.SphereCast(transform.position, _castRadius, _rigidbody.velocity, out RaycastHit hitInfo, castDistance, _hittableLayers))
            if (hitInfo.transform != transform)
                OnHit(hitInfo);

        _lastPosition = transform.position;
    }

    public void EnablePhysic()
    {
        _rigidbody.isKinematic = false;
    }

    public void DisablePhysic()
    {
        _rigidbody.isKinematic = true;
    }

    public void Push(float force)
    {
        Push(transform.forward * force);
    }

    public void Push(Vector3 force)
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    protected virtual void OnHit(RaycastHit hitInfo)
    {
        if (hitInfo.collider.isTrigger && hitInfo.transform.TryGetComponent(out IHealth health) == false)
            return;

        if (TryRicochet(hitInfo))
            return;

        _damagePolicy.TakeDamage(hitInfo, _damage);

        CreateImpactVfx(hitInfo);

        if (hitInfo.rigidbody)
            hitInfo.rigidbody.AddForce(_rigidbody.velocity.normalized * _pushForce, ForceMode.Impulse);

        gameObject.SetActive(false);
    }

    private void CreateImpactVfx(RaycastHit hitInfo)
    {
        ParticleSystem impactVfx;

        if (hitInfo.collider.sharedMaterial == _dirt)
            impactVfx = _dirtVfx;
        else
            impactVfx = _explosionVfx;

        if (impactVfx != null)
            Instantiate(impactVfx, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
    }

    private bool TryRicochet(RaycastHit hitInfo)
    {
        Vector3 direction = transform.position - hitInfo.point;
        float hitAngle = Vector3.Angle(direction, hitInfo.normal);

        if (hitAngle > _ricochetAngle && hitInfo.collider.sharedMaterial == _ricochetMaterial)
        {
            Vector3 invertDirection = hitInfo.point - transform.position;
            Vector3 ricochetDirection = Vector3.Reflect(invertDirection, hitInfo.normal);
            ricochetDirection.Normalize();
            _rigidbody.velocity = ricochetDirection * _rigidbody.velocity.magnitude;
            _detectCollisions = false;

            if (_trailVfx != null)
                _trailVfx.gameObject.SetActive(false);
            if (_ricochetSound != null)
                _audioSource.PlayOneShot(_ricochetSound);

            return true;
        }

        return false;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        StopAllCoroutines();
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_liveTime);
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_damageBehaviour && !(_damageBehaviour is IDamage))
        {
            Debug.LogError(nameof(_damageBehaviour) + " is not implement " + nameof(IDamage));
            _damageBehaviour = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _castRadius);
    }
}