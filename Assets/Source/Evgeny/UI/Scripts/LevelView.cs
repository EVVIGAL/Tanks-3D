using UnityEngine.UI;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;
    [SerializeField] private Image[] _medals;

    private SaveData _data;

    public int LevelToLoad => _levelToLoad;

    private void Awake()
    {
        _data = GetComponentInParent<LevelsKeeper>().Data;        
    }

    private void Start()
    {
        if (_levelToLoad > LevelHolder.CurrentLevel + 1)
            gameObject.SetActive(false);

        for (int i =0; i < _data.Data.Levels[_levelToLoad - 1].CurrentMedals; i++)
        {
            _medals[i].color = Color.yellow;
        }
    }
}