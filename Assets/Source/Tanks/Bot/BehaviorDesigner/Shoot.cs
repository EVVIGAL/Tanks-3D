using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Shoot<TObject, TSharedObject> : Action where TObject : Component where TSharedObject : SharedVariable<TObject>
{
    public MonoBehaviour WeaponBehaviour;
    private IWeapon Weapon => (IWeapon)WeaponBehaviour;

    public TSharedObject TargetObject;
    public SharedUInt Amount = 1;

    private uint _currentAmount;

    public override TaskStatus OnUpdate()
    {
        if (Weapon.CanShoot == false)
            return TaskStatus.Running;

        Weapon.Shoot(TargetObject.Value.transform);
        _currentAmount++;

        if (_currentAmount >= Amount.Value)
            return TaskStatus.Success;

        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        base.OnStart();
        _currentAmount = 0;
    }
}