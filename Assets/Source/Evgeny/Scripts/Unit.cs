using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [Space]
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private bool _isAvailable;

    public int Price => _price;

    public bool IsAvailable => _isAvailable;

    private void Awake()
    {
        _isAvailable = false;
    }
    private void OnEnable()
    {
        _nameText.text = _name;
        _priceText.text = _price.ToString();
    }

    public bool TryBuy(int money)
    {
        if (money < Price)
            return false;

        _isAvailable = true;
        return true;
    }

    public void Unlock()
    {
        _isAvailable = true;
    }
}