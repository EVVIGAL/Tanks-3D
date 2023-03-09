using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private SaveData _data;

    private const string _masterStr = "Master";
    private const string _musicStr = "Music";
    private const string _effectsStr = "Effects";
    private const float _zeroVolume = -80f;
    private const float _fadeSpeed = 60f;
    private const float _waitTime = 0.4f;

    private Coroutine _musicCoroutine;
    private Coroutine _effectsCoroutine;
    private float _effectsValue;
    private float _musicValue;
    private bool _isMute;

    public bool IsMute => _isMute;

    public float MusicValue => _musicValue;

    public float EffectsValue => _effectsValue;

    public void Init()
    {
        _isMute = _data.Data.IsMute;
        _effectsValue = _data.Data.EffectsValue;
        _musicValue = _data.Data.MusicValue;
    }

    public void OnOff()
    {
        _isMute = !_isMute;
        _data.Data.IsMute = _isMute;
        _data.Save();
        _mixer.SetFloat(_masterStr, _isMute ? _zeroVolume : 0);
    }

    public void SetMusic(float value)
    {
        _mixer.SetFloat(_musicStr, value);
        _data.Data.MusicValue = value;
        _data.Save();
    }

    public void SetEffects(float value)
    {
        _mixer.SetFloat(_effectsStr, value);
        _data.Data.EffectsValue = value;
        _data.Save();
    }

    public void Mute()
    {
        _mixer.SetFloat(_masterStr, _zeroVolume);
    }

    public void Load()
    {
        _mixer.SetFloat(_masterStr, _data.Data.IsMute ? _zeroVolume : 0);
    }

    public void FadeIn()
    {
        VolumeFade(_musicValue, _zeroVolume, _musicStr, _musicCoroutine);
        VolumeFade(_effectsValue, _zeroVolume, _effectsStr, _effectsCoroutine);
    }

    public void FadeOut()
    {
        _mixer.SetFloat(_masterStr, _isMute ? _zeroVolume : 0);
        VolumeFade(_zeroVolume, _musicValue, _musicStr, _musicCoroutine, _waitTime);
        VolumeFade(_zeroVolume, _effectsValue, _effectsStr, _effectsCoroutine, _waitTime);
    }

    private void VolumeFade(float startValue, float endValue, string audioType, Coroutine coroutine, float waitTime = 0f)
    {       
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Fade(startValue, endValue, waitTime, audioType));
        }
        else
        {
            coroutine = StartCoroutine(Fade(startValue, endValue, waitTime, audioType));
        }
    }

    private IEnumerator Fade(float startValue, float endValue, float waitTime, string audioType)
    {
        _mixer.SetFloat(audioType, startValue);
        yield return new WaitForSeconds(waitTime);

        while (startValue != endValue)
        {
            startValue = Mathf.MoveTowards(startValue, endValue, Time.unscaledDeltaTime * _fadeSpeed);
            _mixer.SetFloat(audioType, startValue);
            yield return null;
        }

        yield break;
    }
}