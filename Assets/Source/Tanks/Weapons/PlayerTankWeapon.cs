using Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineImpulseSource))]
public class PlayerTankWeapon : TankWeapon
{
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected override void OnShoot(Transform target = null)
    {
        base.OnShoot(target);
        _impulseSource.GenerateImpulse();
    }
}