using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Shoot : Action
{
    public SharedWeapon Weapon;
    public SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Weapon.Value.CanShoot == false)
            return TaskStatus.Running;

        Weapon.Value.Shoot(Target.Value);
        return TaskStatus.Success;
    }
}