using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class ButtonFX : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource _audioSource;

    private Button _button;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_button.interactable)
            _audioSource.Play();
    }
}