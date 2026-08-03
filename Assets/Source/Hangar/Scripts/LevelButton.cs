using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using YG;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private InterAd _ad;
    [SerializeField] private LoadPanel _loadPanel;
    [SerializeField] private TextMeshProUGUI _text;

    private Button _button;
    private int _levelToLoad;

    private void OnEnable()
    {
        YG2.onCloseInterAdv += OnCloseInterAdv;
        YG2.onErrorInterAdv += OnErrorInterAdv;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        YG2.onCloseInterAdv -= OnCloseInterAdv;
        YG2.onErrorInterAdv -= OnErrorInterAdv;
        _button.onClick.RemoveListener(ShowAd);
    }

    public void Init(int level)
    {
        //_button = GetComponent<Button>();
        //_button.onClick.AddListener(ShowAd);
        _levelToLoad = level;
        _text.text = _levelToLoad.ToString();
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
        _audioManager.Load();
        SceneManager.LoadScene(_levelToLoad + 1);
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