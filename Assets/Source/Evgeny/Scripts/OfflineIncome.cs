using UnityEngine;
using System;

public class OfflineIncome : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private TakeButton _income;

    private void OnDisable()
    {
        _data.Data.IncomeTaked = DateTime.UtcNow.ToString();
        _data.Save();
    }

    public void Calculate(float income)
    {
        if (string.IsNullOrEmpty(_data.Data.IncomeTaked))
            return;

        float incomePerSecond = income / 60f;

        DateTime lastSaveTime = DateTime.Parse(_data.Data.IncomeTaked);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;

        double secondPassed = timePassed.TotalSeconds;

        if (secondPassed == 0)
            return;

        float finalIncome = incomePerSecond * (float)secondPassed;
        _income.Add((int)finalIncome);
    }
}