using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedWeapon : SharedVariable<Weapon>
{
    public static implicit operator SharedWeapon(Weapon value) => new SharedWeapon { Value = value };
}