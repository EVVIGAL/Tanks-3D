using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _audioManager.Mute();
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        _audioManager.Load();
    }
}