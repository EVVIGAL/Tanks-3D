using UnityEngine;
using YG;

public class MobileDetector : MonoBehaviour
{
    [SerializeField] private UIJoystick[] _mobileInputUI;

    private void Start()
    {
        if (YG2.envir.device == YG2.Device.Desktop)
            Deactivate();
    }

    private void Deactivate()
    {
        foreach (var mobileInput in _mobileInputUI)
            mobileInput.gameObject.SetActive(false);
    }
}