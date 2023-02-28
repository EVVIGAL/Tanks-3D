using UnityEngine;
using UnityEngine.Events;

public class ObjectTrigger<TObject> : MonoBehaviour where TObject : Component
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private UnityEvent _onObjectEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TObject @object) == false)
            return;

        _onObjectEnter?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _boxCollider.center, _boxCollider.size);
    }
}