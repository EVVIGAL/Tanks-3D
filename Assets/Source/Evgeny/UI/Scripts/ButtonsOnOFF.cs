using UnityEngine.UI;
using UnityEngine;

public class ButtonsOnOFF : MonoBehaviour
{
    [SerializeField] private Button[] _buttonsToOnOff;

    public void OnOff(bool isOn)
    {
        for (int i = 0; i < _buttonsToOnOff.Length; i++)
            _buttonsToOnOff[i].interactable = isOn;
    }
}