using UnityEngine;

public class DamageSource : MonoBehaviour, IHealth
{
    [SerializeField] private float _damageReduce;

    [SerializeField] private MonoBehaviour _healthBehaviour;
    private IHealth _health => (IHealth)_healthBehaviour;

    public bool IsAlive => _health.IsAlive;

    public void TakeDamage(uint damage)
    {
        _health.TakeDamage(damage);
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