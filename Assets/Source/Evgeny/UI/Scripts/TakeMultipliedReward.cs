using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class TakeMultipliedReward : MonoBehaviour
{
    [SerializeField] private FinalReward _final;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private WinReward _winReward;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => ShowAd(_winReward.Take()));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => ShowAd(_winReward.Take()));
    }

    private void ShowAd(int multiplier)
    {
        VideoAd.Show(() => _audioManager.Mute(true), () => Reward(multiplier), () => _audioManager.Load(), null);
    }

    private void Reward(int multiplier)
    {
        _final.Increase(multiplier);
    }
}