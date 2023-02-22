using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Retreat : Action
{
    public SharedMovement SelfMovement;
    public float _direction;
    public SharedFloat _distance;

    private Vector3 _startPosition;

    public override void OnAwake()
    {
        base.OnAwake();
        _startPosition = transform.position;
    }

    public override TaskStatus OnUpdate()
    {
        if (_startPosition.x > transform.position.x || Vector3.Distance(_startPosition, transform.position) < _distance.Value)
            SelfMovement.Value.Move(_direction);

        return TaskStatus.Running;
    }
}