using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

public class LevelCompletedWindow : MonoBehaviour
{
    [SerializeField] private Root _root;
    [SerializeField] private Button _next;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;

    private const int _hubSceneIndex = 1;

    private void OnEnable()
    {
        _next.onClick.AddListener(OnNextButtonClick);
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(OnNextButtonClick);
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
    }

    public void OnNextButtonClick()
    {
        SceneManager.LoadScene((int)_root.CurrentLevelIndex + 1);
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }

    public void OnGoToHangarButtonClick()
    {
        SceneManager.LoadScene(_hubSceneIndex);
    }
}