using Agava.YandexGames;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private ArtilleryAmount _artillery;
    [SerializeField] private ToolsAmount _tools;
    [SerializeField] private Money _money;
    [SerializeField] private int _skillPrice;

    private const int _adAmount = 3;
    private const int _amount = 1; 

    public void OnArtilleryBuyClick()
    {
        if (_money.TrySpend(_skillPrice))
        {
            _artillery.Add(_amount);
        }
    }

    public void OnToolsBuyClick()
    {
        if (_money.TrySpend(_skillPrice))
        {
            _tools.Add(_amount);
        }
    }

    public void OnAdBuyClick()
    {
        ShowAd();
    }

    private void ShowAd()
    {
        VideoAd.Show(() => _audioManager.Mute(), Reward, () => _audioManager.Load(), null);
    }

    private void Reward()
    {
        _artillery.Add(_adAmount);
        _tools.Add(_adAmount);
    }
}