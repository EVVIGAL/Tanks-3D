using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedEnemyCounter : SharedVariable<EnemiesCounter>
{
    public static implicit operator SharedEnemyCounter(EnemiesCounter value) => new SharedEnemyCounter { Value = value };
}