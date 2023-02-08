using UnityEngine;

public static class SaveManager
{
    private const string _moneySaveKey = "Money";

    public static void AddMoney(int value)
    {
        int money = PlayerPrefs.GetInt(_moneySaveKey);
        money += value;
        PlayerPrefs.SetInt(_moneySaveKey, money);
        PlayerPrefs.Save();
    }

    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T Load<T>(string key)
    {
        string loadedString = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(loadedString);
    }
}