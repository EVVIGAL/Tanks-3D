using BehaviorDesigner.Runtime;
using System;

[Serializable]
public class SharedCharacter : SharedVariable<Character>
{
    public static implicit operator SharedCharacter(Character value) => new SharedCharacter { Value = value };
}