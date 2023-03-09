using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [Header("Stats")]
    [SerializeField] private UnitStat _unitStat;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [Space]
    [SerializeField] private Button _battleButton;
    [SerializeField] private TankChoser _choser;
    [SerializeField] private StatsRefresher _refresher;
    [SerializeField] private Button _button;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Money _money;
    [SerializeField] private int _price;
    [SerializeField] private int _neededLevel;

    private const string _neededText = "NeededLvl";

    private int _index;

    public UnitStat UnitStat => _unitStat;

    public string Name => _unitStat.Name;

    public int Price => _price;

    public bool IsAvailable => _unitStat.IsAvailable;

    private void OnEnable()
    {       
        _refresher.SetUnit(_unitStat);
        Refresh();
        _money.ValueChanged += Refresh;
        _button.onClick.AddListener(Buy);
    }

    private void Start()
    {
        _nameText.text = LeanLocalization.GetTranslationText(_unitStat.Name);
    }

    private void OnDisable()
    {
        _money.ValueChanged -= Refresh;
        _button.onClick.RemoveListener(Buy);
        _data.Data.Units[_index] = _unitStat;
    }

    public void Buy()
    {
        if (_money.TrySpend(Price))
        {
            _unitStat.SetAvailable();
            _refresher.SetUnit(_unitStat);
            _choser.Save();
            Refresh();
        }
    }

    public void Set(UnitStat unitStat, int index)
    {
        _unitStat = unitStat;
        _index = index;
    }

    private void Refresh()
    {
        if (_unitStat.IsAvailable)
        {
            _battleButton.interactable = true;
            _priceText.text = string.Empty;
            _button.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(false);
            _data.Data.Units[_index] = _unitStat; 
            return;
        }

        _battleButton.interactable = false;
        _button.gameObject.SetActive(true);

        if (LevelHolder.CurrentLevel > _neededLevel)
        {
            _priceText.text = _price.ToString();
            _lockImage.gameObject.SetActive(false);
            _button.interactable = false;

            if(_money.Value >= Price)
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