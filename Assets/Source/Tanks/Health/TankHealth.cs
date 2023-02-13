using UnityEngine;

public class TankHealth : Health
{
    [SerializeField] private TurretExplosion _turretExplosion;

    [SerializeField] private MonoBehaviour _smokeViewBehaviour;
    private IHealthView _smokeView => (IHealthView)_smokeViewBehaviour;

    [SerializeField] private GetOutFromWay _getOutFromWay;

    protected override void OnHealthChanged()
    {
        base.OnHealthChanged();
        _smokeView.Show(Value, MaxValue);
    }

    protected override void Die()
    {
        base.Die();
        _turretExplosion.Explose();
        _getOutFromWay.GetOut();
    }
}