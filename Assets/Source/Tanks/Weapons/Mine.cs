using UnityEngine;

public class Mine : Explosives
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            return;

        if (IsAlive)
            Explose();
    }
}