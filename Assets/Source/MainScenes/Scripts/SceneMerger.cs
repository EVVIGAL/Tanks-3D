using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMerger : MonoBehaviour
{
    [SerializeField] private string _environmentSceneName;

    private const uint _maxLoadedScenes = 2;

    private void Awake()
    {
        if (SceneManager.sceneCount < _maxLoadedScenes)
            SceneManager.LoadScene(_environmentSceneName, LoadSceneMode.Additive);
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_environmentSceneName));
    }
}