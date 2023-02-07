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

        if (remainderHealthInPercent <= _smokeAppearanceHealth && _smoke.gameObject.activeSelf == false)
            _smoke.gameObject.SetActive(true);

        if (remainderHealthInPercent <= _fireAppearanceHealth && _fire.gameObject.activeSelf == false)
            _fire.gameObject.SetActive(true);
    }
}