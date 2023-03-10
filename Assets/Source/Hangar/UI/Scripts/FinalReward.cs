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
    private const int _bossRewardMultiplier = 5;
    private const int _medalsRewardAdd = 5;

    private TextMeshProUGUI _text;
    private int _levelRewardMultiplier = 35;
    private int _reward;
    private string _rewardStr;
    private bool _isBoss;

    public void Show(int medals)
    {
        _levelRewardMultiplier += _medalsRewardAdd * medals;
        int levelReward = ((int)_root.CurrentLevelIndex - 1) * _levelRewardMultiplier; 

        if(_isBoss)
            levelReward *= _bossRewardMultiplier;

        _reward = levelReward;
        _text.text = _rewardStr + _reward;
        _data.Data.Money += _reward + (int)_wallet.Money - _data.Data.Money;
    }

    public void Increase(int multiplier)
    {
        int additionalReward = (_reward * multiplier) - _reward;
        _text.text = _rewardStr + (additionalReward + _reward);
        _data.Data.Money += additionalReward;
    }

    public void Init(int level, int medals)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rewardStr = LeanLocalization.GetTranslationText(_rewardKey);

        int bossLevelFactor = 10;

        if (level % bossLevelFactor == 0 && medals == 0)
        {
            _isBoss = true;
            return;
        }

        _isBoss = false;
    }
}