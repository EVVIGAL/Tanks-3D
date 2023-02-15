using UnityEngine;

public class EnemyDeathPolicy : MonoBehaviour, IDeathPolicy
{
    [SerializeField] private uint _reward;

    private EnemiesCounter _enemiesCounter;
    private Wallet _playerWallet;

    public void Init(EnemiesCounter enemiesCounter, Wallet wallet)
    {
        _enemiesCounter = enemiesCounter;
        _playerWallet = wallet;
    }

    public void Die()
    {
        if (_enemiesCounter)
            _enemiesCounter.Decrease();
        if (_playerWallet)
            _playerWallet.Add(_reward);
        OnDie();
    }

    protected virtual void OnDie() { }
}