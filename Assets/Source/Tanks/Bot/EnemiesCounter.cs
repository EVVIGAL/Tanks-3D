using System;
using TMPro;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _aliveEnemyCountText;
    [SerializeField] private Root _root;

    private EnemyDeathPolicy[] _enemies;
    private int _aliveEnemyCount;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<EnemyDeathPolicy>();
        foreach (EnemyDeathPolicy enemy in _enemies)
            enemy.Init(this);

        _aliveEnemyCount = _enemies.Length;
        _aliveEnemyCountText.SetText(_aliveEnemyCount.ToString());
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
}