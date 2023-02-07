using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _health;
    [SerializeField] private int _upgradeHealth;
    [SerializeField] private float _damage;
    [SerializeField] private int _upgradeDamage;
    [SerializeField] private float _armor;
    [SerializeField] private int _upgradeArmor;
    [SerializeField] private float _speed;
    [SerializeField] private int _upgradeSpeed;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [Space]
    [SerializeField] private StatsRefresher _refresher;
    [SerializeField] private Button _button;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Money _money;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _neededLevel;
    [SerializeField] private bool _isAvailable;

    private const string _neededText = "NeededLvl";

    public float Health => _health;

    public float Damage => _damage;

    public float Armor => _armor;

    public float Speed => _speed;

    public int UpgradeHealth => _upgradeHealth;

    public int UpgradeDamage => _upgradeDamage;

    public int UpgradeArmor => _upgradeArmor;

    public int UpgradeSpeed => _upgradeSpeed;


    public int Price => _price;

    public bool IsAvailable => _isAvailable;

    private void Awake()
    {
        _isAvailable = false;
    }
    private void OnEnable()
    {
        _nameText.text = _name;
        Refresh();
        _refresher.SetUnit(this);
        _money.ValueChanged += Refresh;
        _button.onClick.AddListener(Buy);
    }

    private void OnDisable()
    {
        _money.ValueChanged -= Refresh;
        _button.onClick.RemoveListener(Buy);
    }

    public void Buy()
    {
        if (_money.Value < Price)
            return;

        _money.Spend(Price);
        _isAvailable = true;
        Refresh();
    }

    public void Unlock()
    {
        _isAvailable = true;
    }

    private void Refresh()
    {
        if (_isAvailable)
        {
            _priceText.text = string.Empty;
            _button.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(false);
            return;
        }

        _button.gameObject.SetActive(true);

        if (LevelHolder.CurrentLevel > _neededLevel)
        {
            _priceText.text = _price.ToString();
            _lockImage.gameObject.SetActive(false);
            _button.interactable = true;
        }
        else
        {
            _priceText.text = LeanLocalization.GetTranslationText(_neededText) + _neededLevel.ToString();
            _lockImage.gameObject.SetActive(true);
            _button.interactable = false;
        }
    }
}