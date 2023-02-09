using UnityEngine.UI;
using UnityEngine;
using System;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private Button _claimButton;
    [SerializeField] private int _rewardValue;

    private const string _lastSaveKey = "LastSaveTime";
    private const int _secondsInDay = 86400;

    private void Start()
    {
        DateTime lastSaveTime = SaveManager.GetDate(_lastSaveKey, DateTime.UtcNow);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondPassed = timePassed.Seconds;

        if (secondPassed == 0)
            return;

        if (secondPassed < _secondsInDay)
        {
            gameObject.SetActive(false);
            return;
        }
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
        SaveManager.SetDate(_lastSaveKey, DateTime.UtcNow);
        _money.Add(_rewardValue);
        gameObject.SetActive(false);
    }
}