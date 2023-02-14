using UnityEngine;

public class �ollisionDamager : MonoBehaviour
{
    [SerializeField] private uint _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth health))
            if (health.IsAlive)
                health.TakeDamage(_damage);
    }
}