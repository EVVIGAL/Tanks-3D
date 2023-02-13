using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChaseObject<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public SharedMovement SelfMovement;
    public TSharedObject TargetObject;

    public override TaskStatus OnUpdate()
    {
        Vector3 positionDifference = TargetObject.Value.transform.position - transform.position;
        positionDifference.y = 0f;
        Vector3 chaseDirection = Vector3.Normalize(positionDifference);
        SelfMovement.Value.Move(-chaseDirection.x);
        return TaskStatus.Running;
    }
}