using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Move : Action
{
    public MonoBehaviour MovementBehaviour;
    public float _direction;

    private IMovement _movement => (IMovement)MovementBehaviour;

    public override TaskStatus OnUpdate()
    {
        _movement.Move(_direction);
        return TaskStatus.Running;
    }
}