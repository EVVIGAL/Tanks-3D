using Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineImpulseSource))]
public class PlayerWeapon : Weapon
{
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected override void OnShoot()
    {
        base.OnShoot();
        _impulseSource.GenerateImpulse();
    }
}