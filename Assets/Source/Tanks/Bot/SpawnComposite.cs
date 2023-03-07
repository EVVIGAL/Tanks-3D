using UnityEngine;

public class SpawnComposite : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemySpawners;

    public bool IsStopped()
    {
        foreach (EnemySpawner spawner in _enemySpawners)
            if (spawner.IsEmpty == false)
                return false;

        return true;
    }
}