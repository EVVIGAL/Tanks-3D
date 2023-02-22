using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class BombShoot : Action
{
    public SharedGameObject _bombTemplate;
    public SharedUInt _damage;
    public SharedVector3 _shootPoint;
    public SharedQuaternion _rotation;

    public override TaskStatus OnUpdate()
    {
        GameObject newBomb = GameObject.Instantiate(_bombTemplate.Value);
        if (newBomb.TryGetComponent(out IProjectile projectile))
            projectile.Init(_damage.Value, _shootPoint.Value, _rotation.Value);

        return TaskStatus.Success;
    }
}