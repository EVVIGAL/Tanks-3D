using UnityEngine;

public class StatsRefresher : MonoBehaviour
{
    [SerializeField] private Stat _health;
    [SerializeField] private Stat _damage;
    [SerializeField] private Stat _armor;
    [SerializeField] private Stat _speed;

    public void SetUnit(Unit unit)
    {
        _health.Set(unit.Health, unit.UpgradeHealth);
        _damage.Set(unit.Damage, unit.UpgradeDamage);
        _armor.Set(unit.Armor, unit.UpgradeArmor);
        _speed.Set(unit.Speed, unit.UpgradeSpeed);
    }
}