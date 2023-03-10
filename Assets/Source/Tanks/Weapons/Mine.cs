using UnityEngine;

public class Mine : Explosives
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.TryGetComponent(out IHealth health) == false)
            return;

        if (other.TryGetComponent(out Ground ground))
            return;

        if (IsAlive)
            Explose();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.isTrigger && collision.transform.TryGetComponent(out IHealth health) == false)
            return;

        if (collision.gameObject.TryGetComponent(out Ground ground))
            return;

        if (IsAlive)
            Explose();
    }
}