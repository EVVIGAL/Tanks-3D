using UnityEngine.UI;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;
    [SerializeField] private Image[] _medals;

    private SaveData _data;
    private Button _button;

    public int LevelToLoad => _levelToLoad;

    private void Awake()
    {
        _data = GetComponentInParent<LevelsKeeper>().Data;    
        _button = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        if (_levelToLoad <= LevelHolder.CurrentLevel)
            _button.interactable = true;

        for (int i =0; i < _data.Data.Levels[_levelToLoad - 1].CurrentMedals; i++)
        {
            _medals[i].color = Color.yellow;
        }
    }
}