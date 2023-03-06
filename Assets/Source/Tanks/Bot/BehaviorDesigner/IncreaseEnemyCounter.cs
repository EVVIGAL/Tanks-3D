using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IncreaseEnemyCounter : Action
{
    public SharedEnemyCounter EnemyCounter;
    public SharedGameObject Enemy;

    public override TaskStatus OnUpdate()
    {
        if (Enemy.Value.TryGetComponent(out BotHealth bot))
        {
            EnemyCounter.Value.Add(bot);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}