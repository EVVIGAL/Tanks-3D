using UnityEngine;

public class BossTankHealth : BotTankHealth
{
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        _healthView?.Show(Value, MaxValue);
    }
}