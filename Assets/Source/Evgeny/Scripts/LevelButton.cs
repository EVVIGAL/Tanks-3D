using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _levelToLoad;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _levelToLoad = Mathf.Clamp(_levelToLoad, 1, SceneManager.sceneCountInBuildSettings - 1);

        if (_levelToLoad >= LevelHolder.CurrentLevel + 1)
            _button.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}