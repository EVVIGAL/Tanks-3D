using UnityEngine;

public class ProjectilePool : ObjectPool<Projectile>
{
    [field: SerializeField] public uint Damage { get; private set; } = 1;

    public void Init(uint damage)
    {
        Damage = damage;
    }

    protected override void OnCreate(Projectile @object)
    {
        if (@object.TryGetComponent(out Projectile projectile))
            projectile.Init(Damage);
    }
}