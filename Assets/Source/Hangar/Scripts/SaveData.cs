using Agava.YandexGames;
using UnityEngine;
using System;

public class SaveData : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private DataHolder _data;
    [SerializeField] private TankChoser _choser;
    [SerializeField] private Root _root;
    [SerializeField] private TakeButton _takeButton;

    public DataHolder Data => _data;

    private const string _leaderboardTxt = "Leaderboard";
    private const string _saveKey = "SaveData";

    private void Awake()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.HasKey(_saveKey))
            Load();

        if (_choser != null)
            _choser.Init(_data.Units, _data.CurrentTankIndex);

        if (_root != null)
            _root.Init(_data.Units[_data.CurrentTankIndex], (uint)_data.CurrentTankIndex);

        if (_takeButton != null)
            _takeButton.Init(_data.LastIncome, _data.Income);

        _audioManager.Init();

        LevelHolder.SetLevel(_data.CurrentLevel);
    }

    private void OnDisable()
    {
        SaveYandex();
        Save();
    }

    public void Save()
    {
        _data.SetMedals();
        SaveManager.Save(_saveKey, _data);
    }

    public void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);
        _data = data;
    }

    public void SetLeaderboardScore()
    {
        int current = _data.Medals;

        Leaderboard.GetPlayerEntry(_leaderboardTxt, (result) =>
        {
            if (current >= result.score)
                SaveBestScore(current);
        });
    }

    private void SaveYandex()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string jsonDataString = JsonUtility.ToJson(_data, true);

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(jsonDataString);
#endif
    }

    private void SaveBestScore(int bestScore)
    {
        Leaderboard.SetScore(_leaderboardTxt, bestScore);
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