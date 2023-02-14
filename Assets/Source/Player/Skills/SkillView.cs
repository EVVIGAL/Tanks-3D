using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _reloadProgressBar;

    public void Show(int amount)
    {
        _button.interactable = amount > 0;
        _amountText.SetText(amount.ToString());
    }

    public void ShowReloadProgress(float reloadTime)
    {
        _reloadProgressBar.fillAmount = reloadTime;
        bool active = Mathf.Approximately(_reloadProgressBar.fillAmount, 1f);
        _button.interactable = active;
        _reloadProgressBar.gameObject.SetActive(!active);
    }
}