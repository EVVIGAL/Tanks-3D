using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedCharacter : SharedVariable<PlayerTank>
{
    public static implicit operator SharedCharacter(PlayerTank value) => new SharedCharacter { Value = value };
}