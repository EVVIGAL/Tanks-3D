using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _aliveEnemyCountText;
    [SerializeField] private Root _root;
    [SerializeField] private SpawnComposite _spawnComposite;

    private List<BotHealth> _enemies = new();
    private bool _spawn;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<BotHealth>().ToList();
        foreach (BotHealth enemy in _enemies)
            enemy.Init(this, _root.PlayerWallet);

        _aliveEnemyCountText.SetText(_enemies.Count.ToString());
    }

    public void Add(BotHealth bot)
    {
        bot.Init(this, _root.PlayerWallet);
        _enemies.Add(bot);
        _aliveEnemyCountText.SetText(_enemies.Count.ToString());
    }

    public void Decrease(BotHealth bot)
    {
        if (_enemies.Contains(bot) == false)
            throw new InvalidOperationException();

        _enemies.Remove(bot);

        if (_enemies.Count == 0)
            if (_spawnComposite == null || (_spawnComposite && _spawnComposite.IsStopped()))
                _root.LevelCompleted();

        _aliveEnemyCountText.SetText(_enemies.Count.ToString());
    }

}