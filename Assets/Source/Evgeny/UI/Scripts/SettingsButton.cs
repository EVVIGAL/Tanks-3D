using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _audioButton;
    private Button _settings;

    private void Awake()
    {
        _settings = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _settings.onClick.AddListener(ShowAudioButton);
    }

    private void OnDisable()
    {
        _settings.onClick.RemoveListener(ShowAudioButton);
    }

    private void ShowAudioButton()
    {
        _audioButton.SetActive(!_audioButton.activeSelf);
    }
}