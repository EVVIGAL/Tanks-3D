using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private GameObject _text;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeSpeed;

    private const float _waitForFadeTime = 0.4f;

    private Coroutine _coroutine;

    private void Start()
    {
        Load(0, Deactivate);
    }

    public void Load(float alpha, UnityAction OnFadingDone)
    {
        _mixer.GetFloat("Music", out float volume);
        _mixer.SetFloat("Music", -80f);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Fade(alpha, volume, OnFadingDone));
        }
        else
        {
            _coroutine = StartCoroutine(Fade(alpha, volume, OnFadingDone));
        }
    }

    private IEnumerator Fade(float alpha, float volume, UnityAction OnFadingDone)
    {
        yield return new WaitForSeconds(_waitForFadeTime);

        if (alpha == 0)
            _text.SetActive(false);

        if (alpha == 1)
            _text.SetActive(true);

        float vol = -80f;

        while (_panel.color.a != alpha || vol != volume)
        {            
            vol = Mathf.MoveTowards(vol, volume, Time.deltaTime * 80f);
            _mixer.SetFloat("Music", vol);
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