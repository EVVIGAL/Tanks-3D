using UnityEngine;

public class BotHealth : Health
{
    [SerializeField] private uint _reward;
    [SerializeField] private int _deathLayer;
    [SerializeField] private GameObject[] _physicObjects;

    private EnemiesCounter _enemiesCounter;
    private Wallet _playerWallet;

    public void Init(EnemiesCounter enemiesCounter, Wallet wallet)
    {
        _enemiesCounter = enemiesCounter;
        _playerWallet = wallet;
    }

    protected override void Die()
    {
        foreach (GameObject @object in _physicObjects)
            @object.layer = _deathLayer;

        if (_enemiesCounter)
            _enemiesCounter.Decrease();
        if (_playerWallet)
            _playerWallet.Add(_reward);
    }
}