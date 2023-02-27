using UnityEngine;

public class GizmosDrawer : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, _height, transform.position.z), _radius);
    }
}