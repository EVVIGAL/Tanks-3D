using System.Collections;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [SerializeField] private float _endVolume;
    [SerializeField] private float _endPitch;
    [SerializeField] private float _speed;

    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private float _startVolume;
    private float _startPitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _startVolume = _audioSource.volume;
        _startPitch = _audioSource.pitch;
    }

    private void OnEnable()
    {
        _audioSource.Play();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Activate());
        }
        else
        {
            _coroutine = StartCoroutine(Activate());
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _audioSource.volume = _startVolume;
        _audioSource.pitch = _startPitch;
        _audioSource.Stop();
    }

    private IEnumerator Activate()
    {
        while (_audioSource.volume != _endVolume || _audioSource.pitch != _endPitch)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _endVolume, Time.deltaTime * _speed);
            _audioSource.pitch = Mathf.MoveTowards(_audioSource.pitch, _endPitch, Time.deltaTime * _speed);
            yield return null;
        }
        yield break;
    }
}