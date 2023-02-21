using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    [SerializeField] private TurretExplosion _turretExplosion;
    [SerializeField] private SmokeDamageView _smokeDamageView;

    private Root _root;

    public void Init(uint maxHealth, uint armor, Root root, MonoBehaviour healthViewBehaviour)
    {
        _root = root;
        _healthViewBehaviour = healthViewBehaviour;
        base.Init(maxHealth, armor);
    }

    protected override void OnTakeDamage()
    {
        _healthView.Show(Value, MaxValue);
        _smokeDamageView.Show(Value, MaxValue);
    }

    protected override void OnHeal()
    {
        _healthView.Show(Value, MaxValue);
        _smokeDamageView.Show(Value, MaxValue);
    }

    protected override void Die()
    {
        _turretExplosion.Explose();

        if (_root)
            _root.GameOver();
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