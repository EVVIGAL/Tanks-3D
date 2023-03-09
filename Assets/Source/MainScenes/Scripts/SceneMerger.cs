using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMerger : MonoBehaviour
{
    [SerializeField] private string _environmentSceneName;
    [SerializeField] private bool _mapSetAsActive;

    private const uint _maxLoadedScenes = 2;

    private void Awake()
    {
        if (SceneManager.sceneCount < _maxLoadedScenes)
            SceneManager.LoadScene(_environmentSceneName, LoadSceneMode.Additive);
    }

    private void Start()
    {
        if(_mapSetAsActive)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_environmentSceneName));
    }
}