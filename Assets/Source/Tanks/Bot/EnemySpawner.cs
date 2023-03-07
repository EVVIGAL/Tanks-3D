using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BotHealth[] _enemies;
    [SerializeField] private EnemiesCounter _enemiesCounter;
    [SerializeField] private Wallet _playerWallet;

    private Queue<BotHealth> _enemiesQueue = new();
    private BotHealth _currentBot;

    public bool IsEmpty => _enemiesQueue.Count == 0;

    private void Start()
    {
        foreach (BotHealth bot in _enemies)
            _enemiesQueue.Enqueue(bot);

        Spawn();
    }

    private void Update()
    {
        if (_currentBot.IsAlive)
            return;

        if (IsEmpty)
        {
            enabled = false;
            return;
        }

        Spawn();
    }

    private void Spawn()
    {
        _currentBot = Instantiate(_enemiesQueue.Dequeue(), transform.position, Quaternion.LookRotation(transform.forward));
        _currentBot.Init(_enemiesCounter, _playerWallet);
        _enemiesCounter.Add(_currentBot);
    }
}