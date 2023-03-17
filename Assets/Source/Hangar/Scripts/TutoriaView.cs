using Agava.YandexGames;
using UnityEngine;

public class TutoriaView : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPC;
    [SerializeField] private GameObject _tutorialMob;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Device.Type == Agava.YandexGames.DeviceType.Desktop)
        {
            _tutorialPC.SetActive(true);
            _tutorialMob.SetActive(false);
            return;
        }

        _tutorialPC.SetActive(false);
        _tutorialMob.SetActive(true);
#else
        _tutorialPC.SetActive(true);
        _tutorialMob.SetActive(false);
#endif
    }
}