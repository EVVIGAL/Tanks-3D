using UnityEngine.UI;
using UnityEngine;

public class TankChoser : MonoBehaviour
{
    [SerializeField] private Unit[] _tanks;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private const string _currentTankIndexTxt = "CurrentTank";

    private int _currentTankIndex;
    private int _visibleTankIndex;

    private void Awake()
    {
        if(PlayerPrefs.HasKey(_currentTankIndexTxt))
            _currentTankIndex = PlayerPrefs.GetInt(_currentTankIndexTxt);
        else
            _currentTankIndex = 0;

        _visibleTankIndex = _currentTankIndex;
    }

    private void Start()
    {
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

        if (_tanks[_visibleTankIndex].IsAvailable)
            Save();
    }

    private void PreviousTank()
    {
        HideAll();
        _tanks[_visibleTankIndex - 1].gameObject.SetActive(true);
        _visibleTankIndex--;
        HideButtons();

        if (_tanks[_visibleTankIndex].IsAvailable)
            Save();
    }

    private void HideAll()
    {
        for (int i = 0; i < _tanks.Length; i++)
            _tanks[i].gameObject.SetActive(false);
    }
    private void Save()
    {
        _currentTankIndex = _visibleTankIndex;
        PlayerPrefs.SetInt(_currentTankIndexTxt, _currentTankIndex);
        PlayerPrefs.Save();
    }

    private void HideButtons()
    {
        if(_visibleTankIndex == 0)
        {
            _leftButton.gameObject.SetActive(false);
            _rightButton.gameObject.SetActive(true);
        }

        if(_visibleTankIndex + 1 == _tanks.Length)
        {
            _rightButton.gameObject.SetActive(false);
            _leftButton.gameObject.SetActive(true);
        }
    }
}