using UnityEngine;

public interface IProjectile
{
    bool IsActive { get; }
    void Init(uint damage, Vector3 position, Quaternion rotation);
    void Push(float force);
    void Enable();
}