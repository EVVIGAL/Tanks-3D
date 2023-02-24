using Agava.YandexGames;
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
    [SerializeField] private UIJoystick[] _mobileInputUI;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Device.Type == Agava.YandexGames.DeviceType.Desktop)
            Deactivate();
#else
        Deactivate();
#endif
    }

    private void Deactivate()
    {
        foreach (var mobileInput in _mobileInputUI)
            mobileInput.gameObject.SetActive(false);
    }
}