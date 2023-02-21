using UnityEngine;

public class RepairKitPickUp : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _healthBehaviour;
    private IHealth _health => (IHealth)_healthBehaviour;
    [SerializeField] private LayerMask _mask;

    [SerializeField] private Vector3 _overlapBoxPosition;
    [SerializeField] private Vector3 _overlapBoxHalfSize;

    private readonly Collider[] _overlapColliders = new Collider[3];

    private void Update()
    {
        int overlapCount = Physics.OverlapBoxNonAlloc(transform.position + transform.TransformVector(_overlapBoxPosition), _overlapBoxHalfSize, _overlapColliders, Quaternion.identity, _mask);
        for (int colliderIterator = 0; colliderIterator < overlapCount; colliderIterator += 1)
        {
            Collider overlapCollider = _overlapColliders[colliderIterator];
            if (overlapCollider.TryGetComponent(out RepairKit repairKit))
            {
                if (_health.IsAlive)
                {
                    _health.Heal(repairKit.Health);
                    Destroy(overlapCollider.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.TransformVector(_overlapBoxPosition), _overlapBoxHalfSize * 2f);
    }

    private void OnValidate()
    {
        if (_healthBehaviour && !(_healthBehaviour is IHealth))
        {
            Debug.LogError(nameof(_healthBehaviour) + " needs to implement " + nameof(IHealth));
            _healthBehaviour = null;
        }
    }
}