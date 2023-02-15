using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private int _levelToLoad;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        if (_levelToLoad > LevelHolder.CurrentLevel + 1)
            _button.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowAd);
    }

    private void ShowAd()
    {
        bool temp = _audioManager.IsMute;
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(() => _audioManager.Mute(true), (temp) => LoadScene(temp), (string error) => LoadScene(temp), () => LoadScene(temp));
#else
        LoadScene(temp);
#endif
    }

    private void LoadScene(bool isMute)
    {
        _audioManager.Mute(isMute);
        SceneManager.LoadScene(_levelToLoad);
    }
}