using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Stat : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    [Space]
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [SerializeField] private Image _upgrade;
    [SerializeField] private Slider _slider;
    [SerializeField] private Money _money;

    private Color _upgradeColor;
    private int _cost;

    private void Awake()
    {
        _upgradeColor = _upgrade.color;
    }

    private void OnEnable()
    {
        _money.ValueChanged += Refresh;
    }

    private void OnDisable()
    {
        _money.ValueChanged -= Refresh;
    }

    public void Set(float value, int upgradeCost)
    {
        _slider.value = value / _maxValue;
        _valueText.text = value.ToString();
        _upgradeText.text = upgradeCost.ToString();
        _cost = upgradeCost;

        if(_cost <= _money.Value)
            _upgrade.color = Color.green;
    }

    private void Refresh()
    {
        if (_cost <= _money.Value)
            _upgrade.color = Color.green;
        else
            _upgrade.color = _upgradeColor;
    }
}