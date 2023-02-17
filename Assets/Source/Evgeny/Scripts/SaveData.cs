using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private DataHolder _data;
    [SerializeField] private TankChoser _choser;
    [SerializeField] private Root _root;

    public DataHolder Data => _data;

    private const string _saveKey = "SaveData";

    private void Awake()
    {
        Load();

        if(_choser != null)
            _choser.Init(_data.Units, _data.CurrentTankIndex);

        if (_root != null)
            _root.Init(_data.Units[_data.CurrentTankIndex], (uint)_data.CurrentTankIndex);

        LevelHolder.SetLevel(_data.CurrentLevel);
        Debug.Log(_data.Levels[0].CurrentMedals);
        Debug.Log(_data.Levels[1].CurrentMedals);
    }

    private void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, _data);
    }

    public void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);

        if (data == default)
            return;

        _data = data;
    }
}

[System.Serializable]
public class DataHolder
{
    public LevelData[] Levels = new LevelData[10];
    public UnitStat[] Units;
    public int Money;
    public int Medals;
    public int TotalIncome;
    public int CurrentLevel;
    public int CurrentTankIndex;
    public int ArtilleryAmount;
    public int ToolsAmount;
    public float MusicValue;
    public float EffectsValue;
    public bool IsMute;
    public string CurrentTankName;
}