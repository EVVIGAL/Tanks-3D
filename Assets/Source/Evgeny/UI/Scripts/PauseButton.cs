using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Pause);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Pause);
    }

    private void Pause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}