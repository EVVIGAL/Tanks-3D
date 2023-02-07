using UnityEngine.Events;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _value;

    private TextMeshProUGUI _valueText;

    public int Value => _value;

    public event UnityAction ValueChanged;

    private void Awake()
    {
        _valueText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Add(int value)
    {
        _value += value;
        Refresh();
    }

    public void Spend(int value)
    {
        _value -= value;
        Refresh();
    }

    private void Refresh()
    {
        ValueChanged?.Invoke();
        _valueText.text = _value.ToString();
    }
}