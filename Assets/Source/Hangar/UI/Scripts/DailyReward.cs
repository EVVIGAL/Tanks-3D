using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ButtonsOnOFF _buttons;
    [SerializeField] private SaveData _data;
    [SerializeField] private Money _money;
    [SerializeField] private Button _claimButton;
    [SerializeField] private int _rewardValue;

    private const int _secondsInDay = 86400;
    private const int _rewardIncrease = 35;

    public int RewardValue => _rewardValue;

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Claim);
        _rewardValue += _rewardIncrease * _data.Data.Medals;
        _text.text = "+ " + _rewardValue.ToString();
    }

    private void Start()
    {
        _buttons.OnOff(false);

        if (string.IsNullOrEmpty(_data.Data.LastDailyReward))
            return;

        DateTime lastSaveTime = DateTime.Parse(_data.Data.LastDailyReward);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        double secondPassed = timePassed.TotalSeconds;

        if (secondPassed == 0)
            return;

        if (secondPassed < _secondsInDay)
            gameObject.SetActive(false);      
    }

    private void OnDisable()
    {       
        _claimButton.onClick.RemoveListener(Claim);
        _buttons.OnOff(true);
    }
    private void Claim()
    {
        _data.Data.LastDailyReward = DateTime.UtcNow.ToString();       
        _money.Add(_rewardValue);
        _data.Data.ToolsAmount++;
        _data.Data.ArtilleryAmount++;
        gameObject.SetActive(false);
    }
}