using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [field: SerializeField] public uint MaxValue { get; private set; }
    [field: SerializeField] public uint Armor { get; private set; }

    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    public uint Value { get; private set; }

    public bool IsAlive => Value > 0;

    public event Action Died;

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
        OnTakeDamage();

        if (Value == 0)
            Die();
    }

    protected virtual void OnTakeDamage()
    {
        _healthView.Show(Value, MaxValue);
    }

    protected virtual void Die()
    {
        Died?.Invoke();
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