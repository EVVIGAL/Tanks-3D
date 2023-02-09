using UnityEngine;

[System.Serializable]
public class UnitStat
{
    [SerializeField] private UnitHealth _health;
    [SerializeField] private UnitDamage _damage;
    [SerializeField] private UnitArmor _armor;
    [SerializeField] private UnitSpeed _speed;

    public UnitHealth Health => _health;

    public UnitDamage Damage => _damage;

    public UnitArmor Armor => _armor;

    public UnitSpeed Speed => _speed;
}

[System.Serializable]
public class Property
{
    [SerializeField] private float _value;
    [SerializeField] private int _maximumValue;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private int _increaseCost;

    public float Value => _value;

    public int MaximumValue => _maximumValue;

    public int UpgradeCost => _upgradeCost;

    public void Upgrade(Money money)
    {
        if (_value >= _maximumValue)
            return;

        if(money.TrySpend(_upgradeCost))
        {
            _value += _upgradeValue;
            _upgradeCost += _increaseCost;
        }
    }
}

[System.Serializable]
public class UnitHealth : Property
{
}

[System.Serializable]
public class UnitDamage : Property
{
}

[System.Serializable]
public class UnitArmor : Property
{
}

[System.Serializable]
public class UnitSpeed : Property
{
}