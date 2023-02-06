using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody == null)
            return;

        hit.rigidbody.AddForce(hit.moveDirection * _force);
    }
}