using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ObjectInRadius<TObject, TSharedObject> : Conditional where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public TSharedObject TargetObject;
    public SharedFloat Radius;

    public override TaskStatus OnUpdate()
    {
        float result = Vector3.Distance(transform.position, TargetObject.Value.transform.position);
        if (result <= Radius.Value)
            return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}