using UnityEngine;

public class HangarBootstrap : MonoBehaviour
{
    public static HangarBootstrap Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (Instance != this)
            return;

        Advertising.AddListener();
    }

    private void OnDisable()
    {
        if (Instance != this)
            return;

        Advertising.RemoveListener();
    }
}