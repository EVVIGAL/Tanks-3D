using Agava.YandexGames;
using UnityEngine;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private PlayerView[] _players;

    private string _leaderboardTxt = "Leaderboard";
    private int _topPlayersCount = 7;
    private int _competingPlayers = 1;

    private void OnEnable()
    {       
        GetLeaderboardEntries();
    }

    private void OnDisable()
    {
        foreach (PlayerView player in _players)
        {
            player.Clear();
            player.gameObject.SetActive(false);
        }
    }

    public void GetLeaderboardEntries()
    {
        Leaderboard.GetEntries(_leaderboardTxt, (result) =>
        {
            for(int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;
                string rank = result.entries[i].rank.ToString();
                string score = result.entries[i].score.ToString();

                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";

                _players[i].gameObject.SetActive(true);
                _players[i].SetView(rank, name, score);
            }
        }, null, _topPlayersCount, _competingPlayers);
    }
}