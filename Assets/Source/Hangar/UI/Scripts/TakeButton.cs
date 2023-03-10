using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class TakeButton : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private MissionPanel _missionPanel;
    [Space]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Money _money;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [Space]
    [SerializeField] private IncomeData _incomeData;

    private Color _upgradeColor = new Color(1, 0.5f, 0, 1);

    private Button _button;
    private float _currentValue;
    private float _income;

    public float Income => _income;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {       
        _text.text = _currentValue.ToString("f0") + " / " + _incomeData.MaximumValue;
        UpdateButton();
        _button.onClick.AddListener(TakeReward);
        _upgradeButton.onClick.AddListener(Upgrade);
        _missionPanel.IncomeChange += OnIncomeChange;
        _money.ValueChanged += UpdateButton;
    }

    public void Init(int value, IncomeData data)
    {
        _incomeData = data;
        OnIncomeChange();
        UpdateButton();
        _currentValue = value;
    }

    private void Update()
    {
        if (_currentValue < _incomeData.MaximumValue)
        {
            _currentValue += Time.deltaTime * _income;
            _text.text = _currentValue.ToString("f0") + " / " + _incomeData.MaximumValue;
        }
    }

    private void OnDisable()
    {
        _data.Data.LastIncome = (int)_currentValue;
        _button.onClick.RemoveListener(TakeReward);
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _missionPanel.IncomeChange -= OnIncomeChange;
        _money.ValueChanged -= UpdateButton;
    }

    public void Add(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, _currentValue, _incomeData.MaximumValue);
    }

    private void TakeReward()
    {
        _money.Add((int)_currentValue);
        _currentValue = 0;
    }

    private void Upgrade()
    {
        if (_money.TrySpend(_incomeData.UpgradeCost))
        {
            _incomeData.MaximumValue += _incomeData.IncreaseMaxValue;
            _incomeData.UpgradeCost += _incomeData.UpgradeCostIncrease;
            _data.Data.MaxIncome = _incomeData.MaximumValue;
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
        if (_incomeData.MaximumValue >= _incomeData.EdgeValue)
            _upgradeButton.gameObject.SetActive(false);

        _upgradeText.text = _incomeData.UpgradeCost.ToString();

        if (_incomeData.UpgradeCost <= _money.Value)
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