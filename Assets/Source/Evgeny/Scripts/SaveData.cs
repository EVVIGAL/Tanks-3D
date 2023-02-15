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
        _data = data;
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
    public bool IsMute;
    public float MusicValue;
    public float EffectsValue;
}