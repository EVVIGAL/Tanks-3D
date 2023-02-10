using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _hoverFX;
    [SerializeField] private AudioClip _clickFX;

    private const string _hoverPath = "Sound/Hover";
    private const string _clickPath = "Sound/Click";

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _hoverFX = Resources.Load<AudioClip>(_hoverPath);
        _clickFX = Resources.Load<AudioClip>(_clickPath);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        _audioSource.PlayOneShot(_clickFX);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        _audioSource.PlayOneShot(_hoverFX);
    }
}