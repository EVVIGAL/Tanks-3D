using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelView : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;
    [SerializeField] private Image _lockImage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image[] _medals;

    private SaveData _data;
    private Button _button;

    private void Awake()
    {
        _data = GetComponentInParent<LevelsKeeper>().Data;    
        _button = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _button.GetComponent<LevelButton>().Init(_levelToLoad);

        if (_levelToLoad <= LevelHolder.CurrentLevel)
        {
            _button.interactable = true;
            _lockImage.gameObject.SetActive(false);
            _text.gameObject.SetActive(true);

            for (int i = 0; i < _medals.Length; i++)
                _medals[i].color = new Color(0, 0, 0, 1f);
        }

        for (int i =0; i < _data.Data.Levels[_levelToLoad - 1].CurrentMedals; i++)
        {
            _medals[i].color = Color.yellow;
        }
    }
}