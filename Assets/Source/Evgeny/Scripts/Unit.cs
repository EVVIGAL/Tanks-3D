using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private UnitStat _unitStat;
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

    public UnitStat UnitStat => _unitStat;

    public int Price => _price;

    public bool IsAvailable => _isAvailable;

    private void Awake()
    {
        _isAvailable = false;

        if(PlayerPrefs.HasKey(_name))
            _unitStat = SaveManager.Load<UnitStat>(_name);
    }

    private void OnEnable()
    {
        _nameText.text = _name;
        _refresher.SetUnit(_unitStat);
        Refresh();
        _money.ValueChanged += Refresh;
        _button.onClick.AddListener(Buy);
    }

    private void OnDisable()
    {
        _money.ValueChanged -= Refresh;
        _button.onClick.RemoveListener(Buy);
        SaveManager.Save(_name, _unitStat);
    }

    public void Buy()
    {
        if (_money.TrySpend(Price))
            _isAvailable = true;
    }

    private void Refresh()
    {
        if (_isAvailable)
        {
            _priceText.text = string.Empty;
            _button.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(false);
            SaveManager.Save(_name, _unitStat);
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