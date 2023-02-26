using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

public class LevelCompletedWindow : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Root _root;
    [SerializeField] private SaveData _data;
    [SerializeField] private Button _next;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;
    [SerializeField] private LoadPanel _loadPanel;

    private const int _hubSceneIndex = 1;

    private void OnEnable()
    {
        _next.onClick.AddListener(ShowAD);
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
        SetLevel();
        _data.Save();
    }

    private void OnDisable()
    {
        _data.SetLeaderboardScore();
        _next.onClick.RemoveListener(ShowAD);
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
    }

    public void OnNextButtonClick()
    {
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene((int)_root.CurrentLevelIndex + 1));
    }

    public void OnRestartButtonClick()
    {
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene(SceneManager.GetSceneAt(0).name));
    }

    public void OnGoToHangarButtonClick()
    {
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene(_hubSceneIndex));
    }

    private void SetLevel()
    {
        if (_root.CurrentLevelIndex >= _data.Data.CurrentLevel)
            _data.Data.CurrentLevel = (int)_root.CurrentLevelIndex;
    }

    private void CloseAd()
    {
        _audioManager.Load();
        OnNextButtonClick();
    }

    private void ShowAD()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(() => _audioManager.Mute(), (bool _) => CloseAd(), null, null);
#endif
    }
}