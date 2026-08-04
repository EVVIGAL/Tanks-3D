using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using YG;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ButtonsOnOFF _buttons;
    [SerializeField] private SaveData _data;
    [SerializeField] private Money _money;
    [SerializeField] private Button _claimButton;
    [SerializeField] private int _rewardValue;

    private const int _rewardIncrease = 35;

    public int RewardValue => _rewardValue;

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Claim);
        _rewardValue += _rewardIncrease * _data.Data.Medals;
        _text.text = "+ " + _rewardValue.ToString();
    }

    private void OnDisable()
    {
        _claimButton.onClick.RemoveListener(Claim);
        _buttons.OnOff(true);
    }

    private void Start()
    {
        _buttons.OnOff(false);

        long startMs = _data.Data.LastDailyReward; // Example timestamp 1
        long endMs = YG2.ServerTime();   // Example timestamp 2

        // Subtract the raw milliseconds
        long differenceMs = endMs - startMs;

        // Convert the millisecond difference into a TimeSpan object
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(differenceMs);

        // Get whole days dropped down (e.g., 2 days)
        int wholeDays = timeSpan.Days;

        if (wholeDays <= 0)
            gameObject.SetActive(false);
    }

    private void Claim()
    {
        _data.Data.LastDailyReward = YG2.ServerTime();       
        _money.Add(_rewardValue);
        _data.Data.ToolsAmount++;
        _data.Data.ArtilleryAmount++;
        gameObject.SetActive(false);
    }
}