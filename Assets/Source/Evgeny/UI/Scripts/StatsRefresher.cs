using UnityEngine;

public class StatsRefresher : MonoBehaviour
{
    [SerializeField] private Stat _health;
    [SerializeField] private Stat _damage;
    [SerializeField] private Stat _armor;
    [SerializeField] private Stat _speed;

    public void SetUnit(UnitStat unit)
    {
        _health.Set(unit.Health);
        _damage.Set(unit.Damage);
        _armor.Set(unit.Armor);
        _speed.Set(unit.Speed);
    }
}