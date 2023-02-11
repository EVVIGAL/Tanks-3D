using Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CinemachineImpulseSource))]
public class PlayerWeaponComposite : WeaponComposite
{
    private CinemachineImpulseSource _impulseSource;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    protected override void OnShoot(Transform target)
    {
        _impulseSource.GenerateImpulse();
    }
}