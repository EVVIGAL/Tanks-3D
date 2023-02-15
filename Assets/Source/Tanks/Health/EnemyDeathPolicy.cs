using UnityEngine;

public class EnemyDeathPolicy : MonoBehaviour, IDeathPolicy
{
    private EnemiesCounter _enemiesCounter;

    public void Init(EnemiesCounter enemiesCounter)
    {
        _enemiesCounter = enemiesCounter;
    }

    public void Die()
    {
        if (_enemiesCounter)
            _enemiesCounter.Decrease();

        // Take rewards
        OnDie();
    }

    protected virtual void OnDie() { }
}