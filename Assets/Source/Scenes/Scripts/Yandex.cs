#pragma warning disable
using UnityEngine.SceneManagement;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    private const string _saveKey = "SaveData";

    public static Yandex Instance;
    private string _language;
    private int _startSceneIndex = 1;

    public string CurrentLanguage => _language;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();
        _language = YandexGamesSdk.Environment.i18n.lang;

        yield return GetData();

        if (YandexGamesSdk.IsInitialized)
            SceneManager.LoadScene(_startSceneIndex);
    }

    private IEnumerator GetData()
    {
        if (PlayerAccount.IsAuthorized)
        {
            string loadedString = "None";
            Debug.Log("Loaded1 = " + loadedString);
            PlayerAccount.GetPlayerData((data) =>
            {
                Debug.Log("Loaded2 = " + data);
                loadedString = data;
                Debug.Log("Loaded3 = " + data);
            });
            Debug.Log("Loaded4 = " + loadedString);
            while (loadedString == "None")
            {
                yield return null;
            }
            Debug.Log("Loaded5 = " + loadedString);
            if (loadedString == "")
                yield break;
            Debug.Log("Loaded6 = " + loadedString);
            DataHolder data = new DataHolder();
            data = JsonUtility.FromJson<DataHolder>(loadedString);
            Debug.Log("Loaded7 = " + data);
            SaveManager.Save(_saveKey, data);
            Debug.Log("Loaded8 = " + data);
        }
    }
}