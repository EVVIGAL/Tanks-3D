using Agava.YandexGames;
using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Button))]
public class TakeMultipliedReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _increasedText;
    [SerializeField] private Button _adButton;
    [SerializeField] private Slider _adSlider;
    [SerializeField] private FinalReward _final;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private WinReward _winReward;

    private const string _translateText = "Reward increased";

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
        VideoAd.Show(() => _audioManager.Mute(), () => Reward(multiplier), () => _audioManager.Load(), null);
    }

    private void Reward(int multiplier)
    {
        _final.Increase(multiplier);
        _increasedText.text = LeanLocalization.GetTranslationText(_translateText) + " X" + multiplier.ToString() + "!";
        _adButton.gameObject.SetActive(false);
        _adSlider.gameObject.SetActive(false);
        _increasedText.gameObject.SetActive(true);
    }
}