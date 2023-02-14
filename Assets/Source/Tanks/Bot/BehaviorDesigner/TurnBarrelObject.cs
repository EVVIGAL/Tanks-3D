using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class TurnBarrelObject<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public SharedBarrel SharedBarrel;
    public TSharedObject TargetObject;
    public SharedAnimationCurve Curve;
    public float _minAngle = 1f;

    public override TaskStatus OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(TargetObject.Value.transform.position, transform.position);
        float angle = Curve.Value.Evaluate(distanceToTarget);
        float currentAngle = SharedBarrel.Value.transform.localEulerAngles.x;
        currentAngle = currentAngle > 180f ? currentAngle - 360f : currentAngle;
        if (Mathf.Abs(currentAngle - angle) <= _minAngle)
            return TaskStatus.Success;

        float direction = currentAngle - angle;
        SharedBarrel.Value.Rotate(direction);
        return TaskStatus.Running;
    }
}