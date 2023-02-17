using System.Globalization;
using Agava.YandexGames;
using UnityEngine;
using System;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, jsonDataString);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(jsonDataString);
#endif
    }

    public static T Load<T>(string key)
    {
        string loadedString = string.Empty;

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.GetPlayerData((data) => loadedString = data);

            if (string.IsNullOrEmpty(loadedString))
                return default;

            return JsonUtility.FromJson<T>(loadedString);

        }
#endif

        if (PlayerPrefs.HasKey(key))
        {
            loadedString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            return default;
        }
    }

    public static void SetDate(string key, DateTime value)
    {
        string convertedToString = value.ToString("u", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(key, convertedToString);
    }

    public static DateTime GetDate(string key, DateTime value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            return value;
        }
    }
}