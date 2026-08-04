using UnityEngine.UI;
using UnityEngine;
using YG;

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
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;

        if (Time.timeScale == 1f)
            YG2.GameplayStart();
        else
            YG2.GameplayStop();
    }
}