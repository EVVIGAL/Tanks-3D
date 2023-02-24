using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private SaveData _data;

    private const string _masterStr = "Master";
    private const string _musicStr = "Music";
    private const string _effectsStr = "Effects";
    private const float _zeroVolume = -80f;

    private float _effectsValue;
    private float _musicValue;
    private bool _isMute;

    public bool IsMute => _isMute;

    public float MusicValue => _musicValue;

    public float EffectsValue => _effectsValue;

    private void Start()
    {
        _isMute = _data.Data.IsMute;
        _effectsValue = _data.Data.EffectsValue;
        _musicValue = _data.Data.MusicValue;
        _mixer.SetFloat(_masterStr, _isMute ? _zeroVolume : 0);
        SetMusic(_effectsValue);
        SetEffects(_musicValue);
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

    public void Mute(bool isMute)
    {
        _mixer.SetFloat(_masterStr, isMute ? _zeroVolume : 0);
    }

    public void Load()
    {
        _mixer.SetFloat(_masterStr, _data.Data.IsMute ? _zeroVolume : 0);
    }
}