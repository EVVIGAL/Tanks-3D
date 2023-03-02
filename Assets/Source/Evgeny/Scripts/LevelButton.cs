using UnityEngine.SceneManagement;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private LoadPanel _loadPanel;
    [SerializeField] private TextMeshProUGUI _text;

    private Button _button;
    private int _levelToLoad;

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowAd);
    }

    public void Init(int level)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowAd);
        _levelToLoad = level;
        _text.text = _levelToLoad.ToString();
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
        _audioManager.Load();
        SceneManager.LoadScene(_levelToLoad + 1);
    }
}