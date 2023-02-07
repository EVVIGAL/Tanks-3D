using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class TankEngine : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private float _idlePitch;
    [SerializeField] private float _movePitch;
    [SerializeField] private float _idleVolume;
    [SerializeField] private float _moveVolume;
    [SerializeField] private float _dampTime;

    private AudioSource _audioSource;
    private float _currentRate;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float targetRate = _movement.CurrentSpeed / _movement.MaxSpeed;
        targetRate = Mathf.Abs(targetRate);
        _currentRate = Mathf.MoveTowards(_currentRate, targetRate, _dampTime * Time.deltaTime);
        _audioSource.pitch = Mathf.Lerp(_idlePitch, _movePitch, _currentRate);
        _audioSource.volume = Mathf.Lerp(_idleVolume, _moveVolume, _currentRate);
    }
}