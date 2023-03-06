using UnityEngine;

public class BotHealth : Health
{
    [SerializeField] private uint _reward;
    [SerializeField] private GameObject[] _physicObjects;
    [field: SerializeField] public int DeathLayer { get; private set; } = 6;

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
            @object.layer = DeathLayer;

        if (_enemiesCounter)
            _enemiesCounter.Decrease(this);
        if (_playerWallet)
            _playerWallet.Add(_reward);
    }
}