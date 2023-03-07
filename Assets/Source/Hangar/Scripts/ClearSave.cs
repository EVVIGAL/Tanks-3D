using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class ClearSave : MonoBehaviour
{
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
        Confirm();

        if(PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData("", Confirm);
    }

    private void Confirm()
    {
        Debug.Log("Deleted");
    }
}