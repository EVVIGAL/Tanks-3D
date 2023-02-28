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
            Debug.Log("1111111111");
            string loadedString = "None";

            PlayerAccount.GetPlayerData((data) =>
            {
                Debug.Log("222222");
                loadedString = data;
            });
            Debug.Log("333333");
            while (loadedString == "None")
            {
                Debug.Log("4444444");
                yield return null;
            }
            Debug.Log("5555555");
            if (string.IsNullOrEmpty(loadedString))
            {
                Debug.Log("888888");
                yield break;
            }
            Debug.Log("6666666");
            DataHolder data = new DataHolder();
            data = JsonUtility.FromJson<DataHolder>(loadedString);
            SaveManager.Save(_saveKey, data);
            Debug.Log("7777777");
        }
    }
}