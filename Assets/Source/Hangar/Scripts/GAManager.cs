using UnityEngine;
using GameAnalyticsSDK;

public class GAManager : MonoBehaviour
{
    void Start()
    {
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Initialize");
    }
}