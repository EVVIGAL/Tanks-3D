using UnityEngine;
using System;

[RequireComponent(typeof(TakeButton))]
public class OfflineIncome : MonoBehaviour
{
    private const string _lastSaveKey = "LastSave";

    private TakeButton _income;

    private void Awake()
    {
        _income = GetComponent<TakeButton>();
    }

    private void OnEnable()
    {
        DateTime lastSaveTime = SaveManager.GetDate(_lastSaveKey, DateTime.UtcNow);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int secondPassed = timePassed.Seconds;

        if (secondPassed == 0)
            return;

        _income.Add((int)(_income.Income * secondPassed));
    }

    private void OnDisable()
    {
        SaveManager.SetDate(_lastSaveKey, DateTime.UtcNow);
    }
}