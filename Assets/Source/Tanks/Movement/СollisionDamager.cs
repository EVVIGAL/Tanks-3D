using UnityEngine;

public class ÑollisionDamager : MonoBehaviour
{
    [SerializeField] private uint _damage;
    [SerializeField] private float _pushForce;
    [SerializeField] private CharacterController _characterController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth health))
            if (health.IsAlive)
                health.TakeDamage(_damage);

        if (other.attachedRigidbody)
            other.attachedRigidbody.AddForce(transform.forward * _pushForce * _characterController.velocity.magnitude, ForceMode.Impulse);
    }
}