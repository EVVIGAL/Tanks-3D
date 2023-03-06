using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _aliveEnemyCountText;
    [SerializeField] private Root _root;

    private List<BotHealth> _enemies = new();
    private int _aliveEnemyCount;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<BotHealth>().ToList();
        foreach (BotHealth enemy in _enemies)
            enemy.Init(this, _root.PlayerWallet);

        Show();
    }

    public void Add(BotHealth bot)
    {
        bot.Init(this, _root.PlayerWallet);
        _enemies.Add(bot);
        Show();
    }

    public void Decrease()
    {
        if (_aliveEnemyCount == 0)
            throw new InvalidOperationException();

        _aliveEnemyCount--;
        _aliveEnemyCountText.SetText(_aliveEnemyCount.ToString());

        if (_aliveEnemyCount == 0)
            _root.LevelCompleted();
    }

    private void Show()
    {
        _aliveEnemyCount = _enemies.Count;
        _aliveEnemyCountText.SetText(_aliveEnemyCount.ToString());
    }
}