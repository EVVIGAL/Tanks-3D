using UnityEngine;

public class SmokeDamageView : MonoBehaviour, IHealthView
{
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private float _smokeAppearanceHealth;
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private float _fireAppearanceHealth;

    public void Show(float health, float maxHealth)
    {
        float remainderHealthInPercent = health / maxHealth * 100f;
        _smoke.gameObject.SetActive(remainderHealthInPercent <= _smokeAppearanceHealth && remainderHealthInPercent > _fireAppearanceHealth);
        _fire.gameObject.SetActive(remainderHealthInPercent <= _fireAppearanceHealth);
    }
}