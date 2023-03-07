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
        _data.Data.ArtilleryAmount += _root.ArtBlowSkill.CurrentAmount;
        _data.Data.ToolsAmount += _root.RepairSkill.CurrentAmount;
        _next.onClick.AddListener(OnNextButtonClick);
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
        SetLevel();
        _data.Save();
    }

    private void OnDisable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _data.SetLeaderboardScore();
#endif
        _next.onClick.RemoveListener(OnNextButtonClick);
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
    }

    public void OnNextButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(() => _audioManager.Mute(), (bool _) => LoadNext(), null, null);
#else
        LoadNext();
#endif
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

    private void LoadNext()
    {
        _audioManager.Load();
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene((int)_root.CurrentLevelIndex + 1));
    }
}