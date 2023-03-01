using Agava.YandexGames;
using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    private void OnEnable()
    {
        if(!PlayerAccount.IsAuthorized)
            gameObject.SetActive(false);
    }
}