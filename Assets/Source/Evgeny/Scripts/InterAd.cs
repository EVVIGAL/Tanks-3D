using UnityEngine.Events;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class InterAd : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    public void ShowAD(UnityAction action)
    {
        bool temp = _audioManager.IsMute;
        InterstitialAd.Show(() => _audioManager.Mute(true),(temp) => OnAdEnd(temp, action), null, () => OnAdEnd(temp, action));
    }

    private void OnAdEnd(bool isMute, UnityAction action)
    {
        _audioManager.Mute(isMute);
        action();
    }
}