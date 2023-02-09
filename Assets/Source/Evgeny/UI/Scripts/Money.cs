using UnityEngine.Events;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _value;

    private TextMeshProUGUI _valueText;

    private const string _saveKey = "Money";

    public int Value => _value;

    public event UnityAction ValueChanged;

    private void Awake()
    {
        _valueText = GetComponent<TextMeshProUGUI>();

        if(PlayerPrefs.HasKey(_saveKey))
            _value = PlayerPrefs.GetInt(_saveKey);
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Add(int value)
    {
        _value += value;
        Refresh();
        PlayerPrefs.SetInt(_saveKey, _value);
        PlayerPrefs.Save();
    }

    public bool TrySpend(int value)
    {
        if (_value < value)
            return false;

        _value -= value;
        Refresh();
        PlayerPrefs.SetInt(_saveKey, _value);
        PlayerPrefs.Save();
        return true;
    }

    private void Refresh()
    {
        ValueChanged?.Invoke();

        if(_valueText != null)
            _valueText.text = _value.ToString();
    }
}