using UnityEngine.UI;
using UnityEngine;

public class TankChoser : MonoBehaviour
{
    [SerializeField] private SaveData _data;
    [SerializeField] private Unit[] _tanks;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private int _currentTankIndex;
    private int _visibleTankIndex;

    private void Start()
    {    
        _visibleTankIndex = _currentTankIndex;
        Refresh();
    }

    private void OnEnable()
    {
        _leftButton.onClick.AddListener(PreviousTank);
        _rightButton.onClick.AddListener(NextTank);
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveListener(PreviousTank);
        _rightButton.onClick.RemoveListener(NextTank);
        Save();
    }

    public void Init(UnitStat[] unitStats, int index)
    {
        _currentTankIndex = index;

        for (int i = 0; i < _tanks.Length; i++)
            _tanks[i].Set(unitStats[i], i);
    } 

    public void Refresh()
    {
        HideAll();
        _tanks[_visibleTankIndex].gameObject.SetActive(true);
        HideButtons();
    }

    private void NextTank()
    {
        HideAll();
        _tanks[_visibleTankIndex + 1].gameObject.SetActive(true);
        _visibleTankIndex++;
        HideButtons();
    }

    private void PreviousTank()
    {
        HideAll();
        _tanks[_visibleTankIndex - 1].gameObject.SetActive(true);
        _visibleTankIndex--;
        HideButtons();
    }

    private void HideAll()
    {
        for (int i = 0; i < _tanks.Length; i++)
            _tanks[i].gameObject.SetActive(false);
    }
    private void Save()
    {
        if (_tanks[_visibleTankIndex].IsAvailable)
        {
            _currentTankIndex = _visibleTankIndex;
            _data.Data.CurrentTankIndex = _currentTankIndex;
            _data.Data.CurrentTankName = _tanks[_currentTankIndex].Name;
        }
    }

    private void HideButtons()
    {
        if (_visibleTankIndex > 0 && _visibleTankIndex + 1 < _tanks.Length)
        {
            _leftButton.gameObject.SetActive(true);
            _rightButton.gameObject.SetActive(true);
            return;
        }

        if (_visibleTankIndex == 0)
        {
            _leftButton.gameObject.SetActive(false);
            _rightButton.gameObject.SetActive(true);
        }

        if (_visibleTankIndex + 1 == _tanks.Length)
        {
            _rightButton.gameObject.SetActive(false);
            _leftButton.gameObject.SetActive(true);
        }
    }
}