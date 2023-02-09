using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class TakeButton : MonoBehaviour
{
    [SerializeField] private MissionPanel _missionPanel;
    [Space]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Money _money;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [Space]
    [SerializeField] private int _maximumValue;
    [SerializeField] private int _increaseMaxValue;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _upgradeCostIncrease;

    private const string _lastSaveKey = "LastSaveTime";

    private Color _upgradeColor;
    private Button _button;
    private float _currentValue;
    private float _income;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _currentValue = 0;
        _upgradeColor = _upgradeButton.GetComponent<Image>().color;
    }

    private void OnEnable()
    {
        OnIncomeChange();
        UpdateButton();
        _text.text = _currentValue.ToString("f0") + " / " + _maximumValue;
        _button.onClick.AddListener(TakeReward);
        _upgradeButton.onClick.AddListener(Upgrade);
        _missionPanel.IncomeChange += OnIncomeChange;
        _money.ValueChanged += UpdateButton;
    }

    private void Update()
    {
        if (_currentValue < _maximumValue)
        {
            _currentValue += Time.deltaTime * _income;
            _text.text = _currentValue.ToString("f0") + " / " + _maximumValue;
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TakeReward);
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _missionPanel.IncomeChange -= OnIncomeChange;
        _money.ValueChanged += UpdateButton;
    }

    private void TakeReward()
    {
        _money.Add((int)_currentValue);
        _currentValue = 0;
    }

    private void Upgrade()
    {
        if (_money.TrySpend(_upgradeCost))
        {
            _maximumValue += _increaseMaxValue;
            _upgradeCost += _upgradeCostIncrease;
        }
    }

    private void UpdateButton()
    {
        _upgradeText.text = _upgradeCost.ToString();

        if (_upgradeCost <= _money.Value)
            _upgradeButton.GetComponent<Image>().color = Color.green;
        else
            _upgradeButton.GetComponent<Image>().color = _upgradeColor;
    }

    private void OnIncomeChange()
    {
        float secondsInMin = 60;
        _income = _missionPanel.TotalIncome/secondsInMin;
    }
}