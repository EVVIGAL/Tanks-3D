using UnityEngine;

public class TankDeathPolicy : MonoBehaviour, IDeathPolicy
{
    [SerializeField] private TurretExplosion _turretExplosion;

    public void Die()
    {
        _turretExplosion.Explose();
        OnDie();
    }

    protected virtual void OnDie() { }
}