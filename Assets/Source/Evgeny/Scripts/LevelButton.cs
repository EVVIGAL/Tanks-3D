using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Object _sceneToLoad;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
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
        if(_sceneToLoad != null)
            SceneManager.LoadScene(_sceneToLoad.name);
    }
}