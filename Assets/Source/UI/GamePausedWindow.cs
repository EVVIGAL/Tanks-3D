using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GamePausedWindow : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;

    private const int _hubSceneIndex = 1;

    private void OnEnable()
    {
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
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