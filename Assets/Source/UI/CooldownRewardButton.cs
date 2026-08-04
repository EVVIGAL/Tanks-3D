using Lean.Localization;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CooldownRewardButton : MonoBehaviour
{
    [Header("Настройки UI")]
    [SerializeField] private string _id;
    [SerializeField] private Button rewardButton;     // Ссылка на саму кнопку
    [SerializeField] private TextMeshProUGUI timerText; // Ссылка на текст таймера (TMP)

    [Header("Настройки времени")]
    [SerializeField] private long cooldownTime = 10; // Время перезарядки в секундах

    [SerializeField] private GameObject _hideObject;

    private void OnEnable()
    {
        rewardButton.onClick.AddListener(OnRewardClick);
    }

    private void OnDisable()
    {
        rewardButton.onClick.RemoveListener(OnRewardClick);
    }

    private void Update()
    {
        if (Time.frameCount % 60 != 0)
            return;

        if (IsCooldown(out long cooldown))
        {
            // Convert to TimeSpan
            TimeSpan ts = TimeSpan.FromMilliseconds(cooldown);

            // Format as hh:mm:ss
            string formattedTime = ts.ToString(@"mm\:ss");

            timerText.text = formattedTime;
            rewardButton.interactable = false;

            timerText.gameObject.SetActive(true);
            _hideObject?.SetActive(false);
        }
        else
        {
            timerText.text = LeanLocalization.GetTranslationText("Get");
            rewardButton.interactable = true;

            timerText.gameObject.SetActive(false);
            _hideObject?.SetActive(true);
        }
    }

    private bool IsCooldown(out long cooldown)
    {
        cooldown = 0;

        if (YG2.saves.RewardReciveDataTimes == null)
            return false;

        RewardReciveDataTime rewardReciveDataTime = YG2.saves.RewardReciveDataTimes.FirstOrDefault(dataTime => dataTime.Id.Equals(_id));
        if (rewardReciveDataTime == null)
            return false;

        if (YG2.ServerTime() > (rewardReciveDataTime.DataTime + cooldownTime))
            return false;

        cooldown = rewardReciveDataTime.DataTime + cooldownTime - YG2.ServerTime();
        return true;
    }

    private void OnRewardClick()
    {
        SaveDataTime();

        // 1. Выдаем награду
       // GiveReward();
    }

    private void SaveDataTime()
    {
        if (YG2.saves.RewardReciveDataTimes == null)
            YG2.saves.RewardReciveDataTimes = new();

        RewardReciveDataTime rewardReciveDataTime = YG2.saves.RewardReciveDataTimes.FirstOrDefault(dataTime => dataTime.Id.Equals(_id));

        if (rewardReciveDataTime != null)
        {
            rewardReciveDataTime.DataTime = YG2.ServerTime();
        }
        else
        {
            rewardReciveDataTime = new(_id, YG2.ServerTime());
            YG2.saves.RewardReciveDataTimes.Add(rewardReciveDataTime);
        }

        YG2.SaveProgress();
    }

    private void GiveReward()
    {
        Debug.Log("Награда получена!");
        // Здесь ваш код выдачи монет, опыта и т.д.
    }
}