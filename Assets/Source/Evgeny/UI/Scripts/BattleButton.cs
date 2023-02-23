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
        bool temp = _audioManager.IsMute;
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(() => _audioManager.Mute(true), (temp) => LoadScene(temp), (string error) => LoadScene(temp), () => LoadScene(temp));
#else
        ActivateLoadPanel(temp);
#endif
    }

    private void ActivateLoadPanel(bool isMute)
    {
        _loadPanel.gameObject.SetActive(true);
        _loadPanel.Load(1, () => LoadScene(isMute));
    }

    private void LoadScene(bool isMute)
    {
        _audioManager.Mute(isMute);
        SceneManager.LoadScene(_lastLevelIndex);
    }
}