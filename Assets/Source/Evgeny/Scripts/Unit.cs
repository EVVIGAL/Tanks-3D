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
    [SerializeField] private int _price;
    [SerializeField] private int _neededLevel;

    private const string _neededText = "NeededLvl";

    public UnitStat UnitStat => _unitStat;

    public string Name => _unitStat.Name;

    public int Price => _price;

    public bool IsAvailable => _unitStat.IsAvailable;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_unitStat.Name))
            _unitStat = SaveManager.Load<UnitStat>(_unitStat.Name);
    }

    private void OnEnable()
    {
        _nameText.text = _unitStat.Name;
        _refresher.SetUnit(_unitStat);
        Refresh();
        _money.ValueChanged += Refresh;
        _button.onClick.AddListener(Buy);
    }

    private void OnDisable()
    {
        _money.ValueChanged -= Refresh;
        _button.onClick.RemoveListener(Buy);
        SaveManager.Save(_unitStat.Name, _unitStat);
    }

    public void Buy()
    {
        if (_money.TrySpend(Price))
        {
            _unitStat.SetAvailable();
            Refresh();
        }
    }

    private void Refresh()
    {
        if (_unitStat.IsAvailable)
        {
            _priceText.text = string.Empty;
            _button.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(false);
            SaveManager.Save(_unitStat.Name, _unitStat);
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