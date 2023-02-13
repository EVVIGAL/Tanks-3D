using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [field: SerializeField] public uint MaxValue { get; private set; }
    [field: SerializeField] public uint Armor { get; private set; }

    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    [SerializeField] private MonoBehaviour _deathPolicyBehaviour;
    private IDeathPolicy _deathPolicy => (IDeathPolicy)_deathPolicyBehaviour;

    public uint Value { get; private set; }

    public bool IsAlive => Value > 0;

    private void Awake()
    {
        Value = MaxValue;
    }

    public void TakeDamage(uint damage)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        uint targetDamage = damage - (damage * Armor / 100);
        Value = (uint)Math.Clamp((int)Value - targetDamage, 0, MaxValue);
        OnHealthChanged();

        if (Value == 0)
            Die();
    }

    public void Heal(uint health)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        Value = Math.Clamp(Value + health, 0, MaxValue);
        OnHealthChanged();
    }

    protected virtual void OnHealthChanged()
    {
        _healthView.Show(Value, MaxValue);
    }

    protected virtual void Die()
    {
        _deathPolicy.Die();
    }

    private void OnValidate()
    {
        if (_healthViewBehaviour && !(_healthViewBehaviour is IHealthView))
        {
            Debug.LogError(nameof(_healthViewBehaviour) + " needs to implement " + nameof(IHealthView));
            _healthViewBehaviour = null;
        }

        if (_deathPolicyBehaviour && !(_deathPolicyBehaviour is IDeathPolicy))
        {
            Debug.LogError(nameof(_deathPolicyBehaviour) + " needs to implement " + nameof(IDeathPolicy));
            _deathPolicyBehaviour = null;
        }
    }
}