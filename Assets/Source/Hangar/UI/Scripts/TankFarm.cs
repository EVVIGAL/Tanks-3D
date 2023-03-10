using UnityEngine.Events;
using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TankFarm : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Money _money;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _upgradeText;

    private const string _localizationKey = "per hour";

    private Color _upgradeColor = new Color(1, 0.5f, 0, 1);

    public Unit Unit => _unit;

    public float FarmRate => _unit.UnitStat.Farm.Value;

    public event UnityAction RateChanged;

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
        _money.ValueChanged += UpdateFarm;
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _money.ValueChanged -= UpdateFarm;
    }

    public void UpdateFarm()
    {
        if (_unit.UnitStat.Farm.Value >= _unit.UnitStat.Farm.MaximumValue)
            _upgradeButton.gameObject.SetActive(false);

        string leanText = LeanLocalization.GetTranslationText(_localizationKey);
        string name = LeanLocalization.GetTranslationText(_unit.Name);
        _mainText.text = $"{name} {_unit.UnitStat.Farm.Value}/{leanText}";
        _upgradeText.text = _unit.UnitStat.Farm.UpgradeCost.ToString();

        if (_unit.UnitStat.Farm.UpgradeCost <= _money.Value)
            _upgradeButton.GetComponent<Image>().color = Color.green;
        else
            _upgradeButton.GetComponent<Image>().color = _upgradeColor;
    }

    private void Upgrade()
    {
        _unit.UnitStat.Farm.Upgrade(_money);
        UpdateFarm();
        RateChanged?.Invoke();
    }
}