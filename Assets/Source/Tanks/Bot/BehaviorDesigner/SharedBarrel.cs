using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedBarrel : SharedVariable<Barrel>
{
    public static implicit operator SharedBarrel(Barrel value) => new SharedBarrel { Value = value };
}