using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private MedalsView _medalsView;

    private const float _excellentPercent = 0.7f;
    private const float _goodPercent = 0.3f;
    private const uint _excellentMedals = 3;
    private const uint _goodMedals = 2;
    private const uint _badMedals = 1;

    private uint _medals;
    private float _maxHealth;
    private float _health;
    private float _excellentHealth;
    private float _goodHealth;

    public void Set(float health)
    {
        if (health > _health)
            return;

        _health = health;

        if (_health >= _excellentHealth)
        {
            _medals = _excellentMedals;
            return;
        }

        if (_health >= _goodHealth && _health <= _excellentHealth)
        {
            _medals = _goodMedals;
            return;
        }

        _medals = _badMedals;
    }

    public void Init(float health)
    {
        _maxHealth = health;
        _health = _maxHealth;
        _excellentHealth = _maxHealth * _excellentPercent;
        _goodHealth = _maxHealth * _goodPercent;
    }

    private void OnEnable()
    {
        _medalsView.Show(_medals);
    }
}