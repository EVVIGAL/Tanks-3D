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
        InterstitialAd.Show(() => _audioManager.Mute(),(bool _) => OnAdEnd(action), null, () => OnAdEnd(action));
    }

    private void OnAdEnd(UnityAction action)
    {
        _audioManager.Load();
        action();
    }
}