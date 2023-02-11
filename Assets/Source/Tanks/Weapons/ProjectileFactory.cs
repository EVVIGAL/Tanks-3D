using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour, IProjectileFactory
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _poolCapacity = 1;

    private List<IProjectile> _bullets = new();

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
            Increase();
    }

    public IProjectile Create()
    {
        IProjectile projectile = _bullets.Find(bullet => bullet.IsActive == false);

        if (projectile == null)
        {
            Increase();
            projectile = _bullets[_bullets.Count - 1];
        }

        projectile.Enable();
        return projectile;
    }

    private void Increase()
    {
        Projectile newBullet = Instantiate(_projectile);
        newBullet.gameObject.SetActive(false);
        _bullets.Add(newBullet);
    }
}