using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;
    [SerializeField] private Button _restore;

    private const int _hubSceneIndex = 1;

    private void OnEnable()
    {
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
        _restore.onClick.AddListener(OnRestoreButtonClick);
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
        _restore.onClick.RemoveListener(OnRestoreButtonClick);
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }

    public void OnGoToHangarButtonClick()
    {
        SceneManager.LoadScene(_hubSceneIndex);
    }

    public void OnRestoreButtonClick()
    {
    }
}