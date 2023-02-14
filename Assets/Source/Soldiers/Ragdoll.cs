using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Collider[] _colliders;

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;

        foreach (Collider collider in _colliders)
            collider.isTrigger = false;
    }

    public void Disable()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;

        foreach (Collider collider in _colliders)
            collider.isTrigger = true;
    }
}