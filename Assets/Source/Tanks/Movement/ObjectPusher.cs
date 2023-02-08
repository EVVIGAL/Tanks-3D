using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    [SerializeField] private uint _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _maxDistance;

    private void Update()
    {
        if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hitInfo, _maxDistance))
            if (hitInfo.transform.TryGetComponent(out Soldier soldier))
                if (soldier.TryGetComponent(out Health health))
                    if (health.IsAlive)
                        health.TakeDamage(_damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * _maxDistance, _radius);
    }
}