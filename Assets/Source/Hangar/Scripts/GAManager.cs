using UnityEngine;
using GameAnalyticsSDK;

public class GAManager : MonoBehaviour
{
    public static GAManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void BattleButton()
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Start", 1f, "Button", "12");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Start button clicked");
        Debug.Log("Start button clicked");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Start button clicked");
    }
}