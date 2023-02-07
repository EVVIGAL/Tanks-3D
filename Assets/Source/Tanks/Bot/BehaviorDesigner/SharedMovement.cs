using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedMovement : SharedVariable<Movement>
{
    public static implicit operator SharedMovement(Movement value) => new SharedMovement { Value = value };
}