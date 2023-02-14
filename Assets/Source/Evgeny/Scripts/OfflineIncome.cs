using UnityEngine;
using System;

public class OfflineIncome : MonoBehaviour
{
    [SerializeField] private TakeButton _income;

    private const string _lastSaveKey = "LastSave";

    private void OnDisable()
    {
        SaveManager.SetDate(_lastSaveKey, DateTime.UtcNow);
    }

    public void Calculate(int income)
    {
        DateTime lastSaveTime = SaveManager.GetDate(_lastSaveKey, DateTime.UtcNow);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondPassed = timePassed.Seconds;

        if (secondPassed == 0)
            return;

        _income.Add(income * secondPassed);
    }
}