using UnityEngine;
using YG;

public class TutoriaView : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPC;
    [SerializeField] private GameObject _tutorialMob;

    private void Start()
    {
        _tutorialPC.SetActive(YG2.envir.device == YG2.Device.Desktop);
        _tutorialMob.SetActive(YG2.envir.device == YG2.Device.Tablet || YG2.envir.device == YG2.Device.Mobile);
    }
}