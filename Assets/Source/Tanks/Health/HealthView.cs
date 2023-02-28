using UnityEngine.UI;
using UnityEngine;

[RequireComponent (typeof(Slider))]
public class HealthView : MonoBehaviour, IHealthView
{
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    public void Show(float health, float maxHealth)
    {
        Debug.Log(health);
        _healthBar.value = health / maxHealth;
    }
}