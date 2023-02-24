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
    [SerializeField] private int _levelToLoad;

    private TextMeshProUGUI _text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _levelToLoad = GetComponentInParent<LevelView>().LevelToLoad;
        _text.text = _levelToLoad.ToString();       
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
        InterstitialAd.Show(() => _audioManager.Mute(true), (bool _) => ActivateLoadPanel(), (string error) => ActivateLoadPanel(), () => ActivateLoadPanel());
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