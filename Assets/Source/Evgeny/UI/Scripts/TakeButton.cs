using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class TakeButton : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private MissionPanel _missionPanel;
    [Space]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Money _money;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [Space]
    [SerializeField] private int _edgeValue;
    [SerializeField] private int _maximumValue;
    [SerializeField] private int _increaseMaxValue;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private int _upgradeCostIncrease;

    private Color _upgradeColor;
    private Button _button;
    private float _currentValue;
    private float _income;

    public float Income => _income;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _upgradeColor = _upgradeButton.GetComponent<Image>().color;
    }

    private void OnEnable()
    {       
        _text.text = _currentValue.ToString("f0") + " / " + _maximumValue;
        _button.onClick.AddListener(TakeReward);
        _upgradeButton.onClick.AddListener(Upgrade);
        _missionPanel.IncomeChange += OnIncomeChange;
        _money.ValueChanged += UpdateButton;
    }

    public void Init()
    {
        OnIncomeChange();
        UpdateButton();
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
        _data.Data.LastIncome = (int)_currentValue;
        _button.onClick.RemoveListener(TakeReward);
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _missionPanel.IncomeChange -= OnIncomeChange;
        _money.ValueChanged -= UpdateButton;
    }

    public void Add(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, _currentValue, _maximumValue);
    }

    private void ShowAD()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        bool temp = _audioManager.IsMute;
        InterstitialAd.Show(() => _audioManager.Mute(true), (temp) => _audioManager.Mute(temp), null, null);
#endif
    }

    private void TakeReward()
    {
        _money.Add((int)_currentValue);
        _currentValue = 0;
        ShowAD();
    }

    private void Upgrade()
    {
        if (_money.TrySpend(_upgradeCost))
        {
            _maximumValue += _increaseMaxValue;
            _upgradeCost += _upgradeCostIncrease;
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
        if (_maximumValue >= _edgeValue)
            _upgradeButton.gameObject.SetActive(false);

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