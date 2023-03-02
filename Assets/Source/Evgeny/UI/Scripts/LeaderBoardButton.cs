using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardButtonView;

    private void OnEnable()
    {
        if(!PlayerAccount.IsAuthorized)
            _leaderboardButtonView.SetActive(false);
    }
}