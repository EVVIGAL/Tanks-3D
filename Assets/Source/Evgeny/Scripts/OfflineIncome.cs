using UnityEngine;
using System;

public class OfflineIncome : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private TakeButton _income;

    private void OnDisable()
    {
        _data.Data.IncomeTaked = DateTime.UtcNow;
        _data.Save();
    }

    public void Calculate(int income, int lastIncome)
    {
        float incomePerSecond = income / 60;
        DateTime lastSaveTime = _data.Data.IncomeTaked;
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondsPassed = timePassed.Seconds;

        if (secondsPassed == 0)
            return;

        _income.Add((int)incomePerSecond * secondsPassed + lastIncome);
    }
}