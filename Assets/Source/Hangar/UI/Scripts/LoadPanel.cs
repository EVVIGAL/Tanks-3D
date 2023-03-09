using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _text;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeSpeed;

    private const float _waitForFadeTime = 0.4f;
    private const float _zeroVolume = -80f;

    private Coroutine _coroutine;

    private void Start()
    {
        Load(0, Deactivate);
    }

    public void Load(float alpha, UnityAction OnFadingDone)
    {
        if (alpha == 1)
            _audioManager.FadeIn();

        if (alpha == 0)
            _audioManager.FadeOut();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Fade(alpha, OnFadingDone));
        }
        else
        {
            _coroutine = StartCoroutine(Fade(alpha, OnFadingDone));
        }
    }

    private IEnumerator Fade(float alpha, UnityAction OnFadingDone)
    {
        if (alpha == 0)
        {
            yield return new WaitForSeconds(_waitForFadeTime);
            _text.SetActive(false);
        }

        if (alpha == 1)
            _text.SetActive(true);

        while (_panel.color.a != alpha)
        {
            _panel.color = new Color(0, 0, 0, Mathf.MoveTowards(_panel.color.a, alpha, Time.unscaledDeltaTime * _fadeSpeed));
            _image.color = new Color(1, 1, 1, Mathf.MoveTowards(_panel.color.a, alpha, Time.unscaledDeltaTime * _fadeSpeed));
            yield return null;
        }

        OnFadingDone();

        yield break;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}