using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject[] _settingsButtons;

    private Button _settings;

    private void Awake()
    {
        _settings = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _settings.onClick.AddListener(ShowSettingsButtons);
    }

    private void OnDisable()
    {
        _settings.onClick.RemoveListener(ShowSettingsButtons);
    }

    private void ShowSettingsButtons()
    {
        for(int i = 0; i < _settingsButtons.Length; i++)
            _settingsButtons[i].SetActive(!_settingsButtons[i].activeSelf);
    }
}