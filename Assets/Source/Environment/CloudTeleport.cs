using UnityEngine;

public class CloudTeleport : MonoBehaviour
{
    [SerializeField] private float _destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cloud cloud))
            cloud.transform.position = new Vector3(_destination, cloud.transform.position.y, cloud.transform.position.z);
    }
}