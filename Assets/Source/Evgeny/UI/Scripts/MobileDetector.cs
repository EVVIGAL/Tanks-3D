using Agava.YandexGames;
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
    [SerializeField] private MobileInputUI _mobileInputUI;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Device.Type == Agava.YandexGames.DeviceType.Desktop)
            _mobileInputUI.gameObject.SetActive(false);
#endif
        _mobileInputUI.gameObject.SetActive(false);
    }
}