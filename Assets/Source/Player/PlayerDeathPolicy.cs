using UnityEngine;

public class PlayerDeathPolicy : MonoBehaviour, IDeathPolicy
{
    [SerializeField] private TurretExplosion _turretExplosion;

    public void Die()
    {
        _turretExplosion.Explose();
    }
}