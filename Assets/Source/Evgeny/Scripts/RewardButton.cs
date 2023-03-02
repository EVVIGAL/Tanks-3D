using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class RewardButton : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Money _money;

    private const int _lvlMultiplier = 20;

    private int _reward = 50000;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _reward += LevelHolder.CurrentLevel * _lvlMultiplier;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowAd);
    }

    private void ShowAd()
    {
        VideoAd.Show(() => _audioManager.Mute(), Reward, () => _audioManager.Load(), null);
    }

    private void Reward()
    {
        _money.Add(_reward);
    }
}