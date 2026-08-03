using UnityEngine;
using System;
using TMPro;
using YG;

public class SaveData : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private DataHolder _data;
    [SerializeField] private TankChoser _choser;
    [SerializeField] private Root _root;
    [SerializeField] private TakeButton _takeButton;
    [SerializeField] private TextMeshProUGUI _medalsText;

    public DataHolder Data => _data;

    private const string _leaderboardTxt = "Leaderboard";

    private void Awake()
    {
        Time.timeScale = 1;

        if (YG2.saves.DataHolder != null)
            _data = YG2.saves.DataHolder;

        if (_choser != null)
            _choser.Init(_data.Units, _data.CurrentTankIndex);

        if (_root != null)
            _root.Init(_data.Units[_data.CurrentTankIndex], (uint)_data.CurrentTankIndex);

        if (_takeButton != null)
            _takeButton.Init(_data.LastIncome, _data.Income);

        _audioManager.Init();
        _data.SetMedals();
        SetLeaderboardScore();
        LevelHolder.SetLevel(_data.CurrentLevel);
    }

    private void OnDisable()
    {
        //Save();
    }

    public void Save()
    {
        YG2.saves.DataHolder = _data;
        YG2.SaveProgress();
    }

    public void SetLeaderboardScore()
    {
        int current = _data.Medals;

        if(_medalsText != null)
            _medalsText.text = current.ToString();

#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(_leaderboardTxt, (result) =>
        {
            if (current >= result.score)
                SaveBestScore(current);
        });
#endif
    }

    private void SaveBestScore(int bestScore)
    {
        //Leaderboard.SetScore(_leaderboardTxt, bestScore);
    }
}

[Serializable]
public class DataHolder
{
    public LevelData[] Levels;
    public UnitStat[] Units;
    public IncomeData Income;
    public string LastDailyReward;
    public string IncomeTaked;
    public int Money;
    public int Medals;
    public int TotalIncome;
    public int LastIncome;
    public int MaxIncome;
    public int CurrentLevel;
    public int CurrentTankIndex;
    public int ArtilleryAmount;
    public int ToolsAmount;
    public float MusicValue;
    public float EffectsValue;
    public bool IsMute;
    public string CurrentTankName;

    public void SetMedals()
    {
        if (Levels.Length <= 0)
            throw new InvalidOperationException();

        Medals = 0;

        foreach (LevelData level in Levels)
            Medals += (int)level.CurrentMedals;
    }
}