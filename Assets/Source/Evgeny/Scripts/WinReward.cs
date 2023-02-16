using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class WinReward : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _speed;
    [SerializeField] private float _minX3;
    [SerializeField] private float _maxX3;
    [SerializeField] private int _rewardX2;
    [SerializeField] private int _rewardX3;

    private Coroutine _coroutine;
    private Slider _slider;
    private bool _isTaked;
    private int _moveTo;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => Take());

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(LeapHandle());
            return;
        }

        _coroutine = StartCoroutine(LeapHandle());
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => Take());

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public int Take()
    {
        _isTaked = true;
        int multiplier = _rewardX2;

        if (_slider.value >= _minX3 && _slider.value <= _maxX3)
        {
            multiplier = _rewardX3;
            return multiplier;
        }

        return multiplier;
    }

    private IEnumerator LeapHandle()
    {
        while (!_isTaked)
        {
            if (_slider.value <= 0)
                _moveTo = 1;

            if (_slider.value >= 1)
                _moveTo = 0;

            _slider.value = Mathf.MoveTowards(_slider.value, _moveTo, _speed * Time.deltaTime);

            yield return null;
        }

        yield break;
    }
}