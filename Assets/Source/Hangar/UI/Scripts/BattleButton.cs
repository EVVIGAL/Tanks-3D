using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using YG;

[RequireComponent(typeof(Button))]
public class BattleButton : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private LoadPanel _loadPanel;

    private const int _notPlayableScenesAmount = 4;

    private Button _button;
    private int _lastLevelIndex;
    private int _endLevelIndex;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _lastLevelIndex = _data.Data.CurrentLevel + 1;
        _endLevelIndex = SceneManager.sceneCountInBuildSettings - _notPlayableScenesAmount;
    }

    private void Start()
    {
        if (_lastLevelIndex >= _endLevelIndex)
            _lastLevelIndex = _endLevelIndex;        
    }

    private void OnEnable()
    {
        YG2.onCloseInterAdv += OnCloseInterAdv;
        YG2.onErrorInterAdv += OnErrorInterAdv;
        _button.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        YG2.onCloseInterAdv -= OnCloseInterAdv;
        YG2.onErrorInterAdv -= OnErrorInterAdv;
        _button.onClick.RemoveListener(ShowAd);
    }

    private void ShowAd()
    {
        YG2.InterstitialAdvShow();
    }

    private void ActivateLoadPanel()
    {
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => LoadScene());
    }

    private void LoadScene()
    {
        if (_lastLevelIndex > _data.Data.Levels.Length + 1)
            _lastLevelIndex = _data.Data.Levels.Length + 1;

        _audioManager.Load();
        SceneManager.LoadScene(_lastLevelIndex);
    }

    private void OnErrorInterAdv()
    {
        ActivateLoadPanel();
    }

    private void OnCloseInterAdv()
    {
        ActivateLoadPanel();
    }
}