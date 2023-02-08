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
    [Space]
    [SerializeField] private int _farmRate;
    [SerializeField] private int _farmRateIncrease;
    [SerializeField] private int _upgradeCost;

    private const string _saveKey = "FarmRate";
    private const string _localizationKey = "per hour";

    private Color _upgradeColor;

    public Unit Unit => _unit;

    public int FarmRate => _farmRate;

    public event UnityAction RateChanged;

    private void Awake()
    {
        _upgradeColor = _upgradeButton.GetComponent<Image>().color;

        if (PlayerPrefs.HasKey(_saveKey + _unit.Name))
            _farmRate = PlayerPrefs.GetInt(_saveKey + _unit.Name);
    }

    private void OnEnable()
    {
        UpdateFarm();
        _upgradeButton.onClick.AddListener(Upgrade);
        _money.ValueChanged += UpdateFarm;
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _money.ValueChanged -= UpdateFarm;
    }

    private void Upgrade()
    {
        if (_money.TrySpend(_upgradeCost))
        {
            _farmRate += _farmRateIncrease;
            UpdateFarm();
            RateChanged?.Invoke();
            PlayerPrefs.SetInt(_saveKey + _unit.Name, _farmRate);
            PlayerPrefs.Save();
        }
    }

    private void UpdateFarm()
    {
        string leanText = LeanLocalization.GetTranslationText(_localizationKey);
        _mainText.text = $"{_unit.Name} {_farmRate}/{leanText}";
        _upgradeText.text = _upgradeCost.ToString();

        if (_upgradeCost <= _money.Value)
            _upgradeButton.GetComponent<Image>().color = Color.green;
        else
            _upgradeButton.GetComponent<Image>().color = _upgradeColor;
    }
}