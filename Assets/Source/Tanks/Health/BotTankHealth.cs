using UnityEngine;

[RequireComponent (typeof(GetOutFromWay), typeof(SmokeDamageView))]
public class BotTankHealth : BotHealth
{
    [SerializeField] private TurretExplosion _turretExplosion;

    private GetOutFromWay _getOutFromWay;
    private SmokeDamageView _smokeDamageView;

    private void Start()
    {
        _getOutFromWay = GetComponent<GetOutFromWay>();
        _smokeDamageView = GetComponent<SmokeDamageView>();
    }

    protected override void OnTakeDamage()
    {
        _smokeDamageView.Show(Value, MaxValue);
    }

    protected override void Die()
    {
        base.Die();
        _turretExplosion.Explose();
        _getOutFromWay.GetOut();
        _smokeDamageView.Stop();
    }
}