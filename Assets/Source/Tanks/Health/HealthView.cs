using UnityEngine.UI;
using UnityEngine;

[RequireComponent (typeof(Slider))]
public class HealthView : MonoBehaviour, IHealthView
{
    [SerializeField] private DamageCounter _damageCounter;
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    public void Show(float health, float maxHealth)
    {
        _healthBar.value = health / maxHealth;
        _damageCounter.Set(health);
    }
}