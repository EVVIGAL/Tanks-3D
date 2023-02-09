using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Shoot<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public SharedWeapon Weapon;
    public TSharedObject TargetObject;

    public override TaskStatus OnUpdate()
    {
        if (Weapon.Value.CanShoot == false)
            return TaskStatus.Running;

        Weapon.Value.Shoot(TargetObject.Value.transform);
        return TaskStatus.Success;
    }
}