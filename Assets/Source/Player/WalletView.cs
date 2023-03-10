using System.Collections;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _addedMoneyText;
    [SerializeField] private float _addedMoneyShowTime = 2f;

    private uint _currentMoney;
    private Coroutine _coroutine;
    private const string Prefix = "+";

    public void Init(uint money)
    {
        _currentMoney = money;
        _moneyText.SetText(_currentMoney.ToString());
    }

    public void Show(uint money, uint addedMoney)
    {
        _currentMoney = money;
        _addedMoneyText.SetText(Prefix + addedMoney.ToString());
        _addedMoneyText.gameObject.SetActive(true);

        if (_coroutine == null)
            StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_addedMoneyShowTime);
        _addedMoneyText.gameObject.SetActive(false);
        _moneyText.SetText(_currentMoney.ToString());
        _coroutine = null;
    }
}