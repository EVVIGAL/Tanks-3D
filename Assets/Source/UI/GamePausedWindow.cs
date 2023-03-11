using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GamePausedWindow : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;
    [SerializeField] private LoadPanel _loadPanel;

    private const int _hubSceneIndex = 1;

    private int _artilleryAmount;
    private int _toolsAmount;

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
        _data.Data.ArtilleryAmount += _artilleryAmount;
        _data.Data.ToolsAmount += _toolsAmount;
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene(SceneManager.GetSceneAt(0).name));
    }

    public void OnGoToHangarButtonClick()
    {
        _data.Data.ArtilleryAmount += _artilleryAmount;
        _data.Data.ToolsAmount += _toolsAmount;
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => SceneManager.LoadScene(_hubSceneIndex));      
    }

    public int SetArtilleryAmount(int artillery)
    {
        _artilleryAmount = artillery;
        return _artilleryAmount;
    }

    public int SetToolsAmount(int tools)
    {
        _toolsAmount = tools;
        return _toolsAmount;
    }
}