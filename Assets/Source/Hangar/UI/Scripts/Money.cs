using UnityEngine.Events;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Money : MonoBehaviour
{
    [SerializeField] private SaveData _saveData;

    private TextMeshProUGUI _valueText;
    private int _value;

    public int Value => _value;

    public event UnityAction ValueChanged;

    private void Awake()
    {
        _valueText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _value = _saveData.Data.Money;
        Refresh();
    }

    public void Add(int value)
    {
        _value += value;
        Refresh();
    }

    public bool TrySpend(int value)
    {
        if (_value < value)
            return false;

        _value -= value;
        Refresh();
        return true;
    }

    private void Refresh()
    {
        ValueChanged?.Invoke();

        if(_valueText != null)
            _valueText.text = _value.ToString();

        _saveData.Data.Money = _value;
        _saveData.Save();
    }
}