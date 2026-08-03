using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using YG;

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
        YG2.onCloseInterAdv += OnCloseInterAdv;
        YG2.onErrorInterAdv += OnErrorInterAdv;
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
        YG2.onCloseInterAdv -= OnCloseInterAdv;
        YG2.onErrorInterAdv -= OnErrorInterAdv;
        _data.SetLeaderboardScore();
        _next.onClick.RemoveListener(OnNextButtonClick);
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
    }

    public void OnNextButtonClick()
    {
        YG2.InterstitialAdvShow();
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

    private void LoadNextLevel()
    {
        _audioManager.Load();
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene((int)_root.CurrentLevelIndex + 1));
    }

    private void OnCloseInterAdv()
    {
        LoadNextLevel();
    }

    private void OnErrorInterAdv()
    {
        LoadNextLevel();
    }
}