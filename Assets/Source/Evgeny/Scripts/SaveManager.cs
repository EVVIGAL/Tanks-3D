using UnityEngine;

public static class SaveManager
{
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