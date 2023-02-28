using System.Collections.Generic;
using UnityEngine;

public class EnemyArtBlow : Weapon
{
    private List<Transform> _shootPoints = new();
    private int _currentShootPoint;

    private void Start()
    {
        _shootPoints.AddRange(GetComponentsInChildren<Transform>());
        _shootPoints.RemoveAt(0);
    }

    private void Update()
    {
        if (WeaponReloader.CanShoot == false)
            return;

        Shoot();
        _currentShootPoint++;
        if (_currentShootPoint >= _shootPoints.Count)
            enabled = false;
    }

    protected override Transform GetShootPoint()
    {
        return _shootPoints[_currentShootPoint];
    }
}