using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private Root _root;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toHangar;
    [SerializeField] private Button _restore;
    [SerializeField] private LoadPanel _loadPanel;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _inputPanel;
    [SerializeField] private EnemiesCounter _counter;

    private const int _hubSceneIndex = 1;

    private void OnEnable()
    {
        _data.Data.ArtilleryAmount += _root.ArtBlowSkill.CurrentAmount;
        _data.Data.ToolsAmount += _root.RepairSkill.CurrentAmount;
        _restart.onClick.AddListener(OnRestartButtonClick);
        _toHangar.onClick.AddListener(OnGoToHangarButtonClick);
        _restore.onClick.AddListener(OnRestoreButtonClick);
        int reward = (int)_wallet.Money - _data.Data.Money;
        _data.Data.Money += reward;
    }

    private void Start()
    {
        if(_counter.GetEnemies() <= 0)
            _restore.gameObject.SetActive(false);
    }   

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _toHangar.onClick.RemoveListener(OnGoToHangarButtonClick);
        _restore.onClick.RemoveListener(OnRestoreButtonClick);
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

    public void OnRestoreButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowAd();
#else
        Restore();
#endif
    }

    private void ShowAd()
    {
        VideoAd.Show(() => Pause(), () => Restore(), () => Unpause(), (string _) => Unpause());
    }

    private void Pause()
    {
        _audioManager.Mute();
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        _audioManager.Load();
        Time.timeScale = 1;
    }

    private void Restore()
    {
        _root.CreatePlayerTank();
        _inputPanel.SetActive(true);
        gameObject.SetActive(false);
        _restore.gameObject.SetActive(false);
    }
}