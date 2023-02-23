using UnityEngine.UI;
using UnityEngine;
using System;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private Money _money;
    [SerializeField] private Button _claimButton;
    [SerializeField] private int _rewardValue;

    private const int _secondsInDay = 15;

    private void Start()
    {
        if (string.IsNullOrEmpty(_data.Data.LastDailyReward))
            return;

        DateTime lastSaveTime = DateTime.Parse(_data.Data.LastDailyReward);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondPassed = timePassed.Seconds;

        if (secondPassed == 0)
            return;

        if (secondPassed < _secondsInDay)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Claim);
    }

    private void OnDisable()
    {       
        _claimButton.onClick.RemoveListener(Claim);
    }
    private void Claim()
    {
        _data.Data.LastDailyReward = DateTime.UtcNow.ToString();
        _money.Add(_rewardValue);
        _data.Data.ToolsAmount++;
        gameObject.SetActive(false);
    }
}