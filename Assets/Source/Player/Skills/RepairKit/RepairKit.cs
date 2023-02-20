using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]
public class RepairKit : MonoBehaviour
{
    [field: SerializeField] public uint Health;
    [SerializeField] private UnityEvent _onCollisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        _onCollisionEnter?.Invoke();
    }
}