using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private DataHolder _data;

    public DataHolder Data => _data;

    private string _saveKey = "SaveData";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_saveKey))
            Load();

        LevelHolder.SetLevel(_data.CurrentLevel);
    }

    private void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, _data);
    }

    private void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);
        _data.Money = data.Money;
        _data.TotalIncome = data.TotalIncome;
        _data.CurrentLevel = data.CurrentLevel;
        _data.ArtilleryAmount = data.ArtilleryAmount;
        _data.ToolsAmount = data.ToolsAmount;
    }
}

[System.Serializable]
public class DataHolder
{
    public int Money;
    public int TotalIncome;
    public int CurrentLevel;
    public int ArtilleryAmount;
    public int ToolsAmount;
}