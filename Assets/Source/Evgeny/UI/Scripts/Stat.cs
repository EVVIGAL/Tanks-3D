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

    private Button _upgradeButton;
    private Color _upgradeColor;
    private float _cost;

    private void Awake()
    {
        _upgradeButton = _upgrade.GetComponent<Button>();
        _upgradeColor = _upgrade.color;
    }

    private void OnEnable()
    {
        _money.ValueChanged += UpdateButton;
    }

    private void OnDisable()
    {
        _money.ValueChanged -= UpdateButton;
    }

    public void Set(Property property)
    {
        Refresh(property);       
        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(() => property.Upgrade(_money));
        _upgradeButton.onClick.AddListener(() => Refresh(property));
    }

    private void Refresh(Property property)
    {
        _slider.value = property.Value / _maxValue;
        _valueText.text = property.Value.ToString();
        _upgradeText.text = property.UpgradeCost.ToString();
        _cost = property.UpgradeCost;

        if(property.Value >= property.MaximumValue)
        {
            _upgradeButton.gameObject.SetActive(false);
            return;
        }

        UpdateButton();
    }

    private void UpdateButton()
    {
        if (_cost <= _money.Value)
            _upgrade.color = Color.green;
        else
            _upgrade.color = _upgradeColor;
    }
}