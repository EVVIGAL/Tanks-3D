using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class BattleButton : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private LoadPanel _loadPanel;

    private Button _button;
    private int _lastLevelIndex;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _lastLevelIndex = _data.Data.CurrentLevel + 1;
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
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(() => _audioManager.Mute(), (bool _) => ActivateLoadPanel(), (string error) => ActivateLoadPanel(), () => ActivateLoadPanel());
#else
        ActivateLoadPanel();
#endif
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
}