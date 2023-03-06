using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToTarget : Action
{
    public SharedFloat _speed;
    public SharedGameObject _target;
    public float _stoppingDistance = 1f;

    public override TaskStatus OnUpdate()
    {
        Vector3 direction = _target.Value.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        if (Vector3.Distance(transform.position, _target.Value.transform.position) <= _stoppingDistance)
            return TaskStatus.Success;

        transform.position = Vector3.MoveTowards(transform.position, _target.Value.transform.position, _speed.Value * Time.deltaTime);
        return TaskStatus.Running;
    }
}