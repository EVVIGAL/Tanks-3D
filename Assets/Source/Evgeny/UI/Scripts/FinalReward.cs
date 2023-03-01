using Lean.Localization;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FinalReward : MonoBehaviour
{
    [SerializeField] private Root _root;
    [SerializeField] private SaveData _data;
    [SerializeField] private Wallet _wallet;

    private const string _rewardKey = "Your reward";
    private const int _levelRewardMultiplier = 50;

    private TextMeshProUGUI _text;
    private int _reward;
    private string _rewardStr;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rewardStr = LeanLocalization.GetTranslationText(_rewardKey);
    }

    private void OnEnable()
    {
        int levelReward = ((int)_root.CurrentLevelIndex - 1) * _levelRewardMultiplier; 
        _reward = (int)_wallet.Money + levelReward;
        _text.text = _rewardStr + _reward;
        _data.Data.Money += _reward;
    }

    public void Increase(int multiplier)
    {
        int additionalReward = (_reward * multiplier) - _reward;
        _text.text = _rewardStr + (additionalReward + _reward);
        _data.Data.Money += additionalReward;
    }
}