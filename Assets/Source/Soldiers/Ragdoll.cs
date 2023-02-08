using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
    }

    public void Disable()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;
    }
}