using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonFX : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource _audioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        _audioSource.Play();
    }
}