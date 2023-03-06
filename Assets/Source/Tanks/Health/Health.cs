using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [field: SerializeField] public uint MaxValue { get; private set; }
    [field: SerializeField] public uint Armor { get; private set; }

    public uint Value { get; private set; }

    public bool IsAlive => Value > 0;

    private void Awake()
    {
        Value = MaxValue;
    }

    public void Init(uint maxHealth, uint armor)
    {
        MaxValue = maxHealth;
        Armor = armor;
        Value = MaxValue;
    }

    public uint TakeDamage(uint damage)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        uint targetDamage = damage - (damage * Armor / 100);
        Value = (uint)Math.Clamp((int)Value - targetDamage, 0, MaxValue);
        OnTakeDamage();

        if (Value == 0)
            Die();

        return targetDamage;
    }

    public void Heal(uint health)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        Value = Math.Clamp(Value + health, 0, MaxValue);
        OnHeal();
    }

    protected virtual void OnTakeDamage() { }

    protected virtual void OnHeal() { }

    protected virtual void Die() { }
}