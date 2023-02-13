using UnityEngine;

public class DamageReceiver : MonoBehaviour, IHealth
{
    [SerializeField] private float _damageMultiplier = 1f;

    [SerializeField] private MonoBehaviour _healthBehaviour;
    private IHealth _health => (IHealth)_healthBehaviour;

    public bool IsAlive => _health.IsAlive;

    public void Heal(uint health) => _health.Heal(health);

    public uint TakeDamage(uint damage)
    {
        return _health.TakeDamage((uint)(damage * _damageMultiplier));
    }

    private void OnValidate()
    {
        if (_healthBehaviour && !(_healthBehaviour is IHealth))
        {
            Debug.LogError(nameof(_healthBehaviour) + " needs to implement " + nameof(IHealth));
            _healthBehaviour = null;
        }
    }
}