using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonFX : MonoBehaviour, IPointerDownHandler
{
    private AudioSource _audioSource;
    private AudioClip _clickFX;

    private const string _clickPath = "Sound/Click";

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _clickFX = Resources.Load<AudioClip>(_clickPath);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(_clickFX);
    }
}