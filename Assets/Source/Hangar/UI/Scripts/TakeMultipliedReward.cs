using Lean.Localization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using YG;

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
        _button.onClick.AddListener(() => ShowAd(2));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => ShowAd(2));
    }

    private void ShowAd(int multiplier)
    {
        YG2.RewardedAdvShow(string.Empty, () => Reward(multiplier));
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