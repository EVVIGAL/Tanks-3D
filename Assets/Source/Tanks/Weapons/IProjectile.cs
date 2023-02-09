using UnityEngine;

public interface IProjectile
{
    bool IsActive { get; }
    void Init(uint damage, Vector3 position, Quaternion rotation, Transform parent = null);
    void Push(float force);
    void Push(Vector3 force);
    void Enable();
    void Disable();
}