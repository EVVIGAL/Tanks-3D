using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private Button _button;

    public void Show(int amount)
    {
        _button.interactable = amount > 0;
        _amountText.SetText(amount.ToString());
    }
}