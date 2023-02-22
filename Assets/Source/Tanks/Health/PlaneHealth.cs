using UnityEngine;

[RequireComponent (typeof(SmokeDamageView))]
public class PlaneHealth : BotHealth
{
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    [SerializeField] private SmokeDamageView[] _smokeDamageView;
    [SerializeField] private Rotater[] _propellers;

    protected override void OnTakeDamage()
    {
        _healthView.Show(Value, MaxValue);
        foreach (SmokeDamageView smokeView in _smokeDamageView)
            smokeView.Show(Value, MaxValue);
    }

    protected override void Die()
    {
        base.Die();
        foreach (Rotater rotater in _propellers)
            rotater.enabled = false;
    }

    private void OnValidate()
    {
        if (_healthViewBehaviour && !(_healthViewBehaviour is IHealthView))
        {
            Debug.LogError(nameof(_healthViewBehaviour) + " needs to implement " + nameof(IHealthView));
            _healthViewBehaviour = null;
        }
    }
}