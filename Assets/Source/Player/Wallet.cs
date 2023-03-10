using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;

    public uint Money { get;private set; }

    public void Init(uint money)
    {
        Money = money;
        _walletView.Init(money);
    }

    public void Add(uint amount)
    {
        Money += amount;
        _walletView.Show(Money, amount);
    }
}