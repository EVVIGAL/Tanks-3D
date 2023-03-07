using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardButtonView;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(!PlayerAccount.IsAuthorized)
            _leaderboardButtonView.SetActive(false);
#endif
    }
}