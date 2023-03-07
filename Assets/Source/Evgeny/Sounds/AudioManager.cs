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
    private const float _fadeSpeed = 80f;

    private Coroutine _coroutine;
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
        _mixer.SetFloat(_masterStr, _isMute ? _zeroVolume : 0);
        SetMusic(_musicValue);
        SetEffects(_effectsValue);      
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

    public void VolumeFade(float startValue, float endValue, float waitTime = 0f)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Fade(startValue, endValue, waitTime));
        }
        else
        {
            _coroutine = StartCoroutine(Fade(startValue, endValue, waitTime));
        }
    }

    private IEnumerator Fade(float startValue, float endValue, float waitTime)
    {
        _mixer.SetFloat(_masterStr, startValue);
        yield return new WaitForSeconds(waitTime);

        while (startValue != endValue)
        {
            startValue = Mathf.MoveTowards(startValue, endValue, Time.unscaledDeltaTime * _fadeSpeed);
            _mixer.SetFloat(_masterStr, startValue);
            yield return null;
        }

        yield break;
    }
}