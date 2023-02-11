using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public float ReloadSpeed { get; private set; }
    [field: SerializeField] public uint MagazineSize { get; private set; }

    [SerializeField] private MonoBehaviour _weaponCompositeBehaviour;
    private IWeaponComposite _weaponComposite => (IWeaponComposite)_weaponCompositeBehaviour;

    public bool CanShoot => IsEmpty == false && _isReloading == false;

    public bool IsEmpty => _ammoInMagazine == 0;

    private uint _ammoInMagazine;
    private bool _isReloading;

    private void Awake()
    {
        _ammoInMagazine = MagazineSize;
    }

    public void Shoot(Transform target = null)
    {
        if (CanShoot == false)
            throw new InvalidOperationException();

        _weaponComposite.Shoot(target);
        _isReloading = true;
        _ammoInMagazine--;
        Reload();
    }

    private void Reload()
    {
        if (IsEmpty)
        {
            _weaponComposite.Reload();
            StartCoroutine(Wait(ReloadSpeed, () =>
            {
                _ammoInMagazine = MagazineSize;
                _isReloading = false;
            }));
        }
        else
        {
            StartCoroutine(Wait(RateOfFire, () => _isReloading = false));
        }
    }

    private IEnumerator Wait(float time, Action onSuccessCallback)
    {
        yield return new WaitForSeconds(time);
        onSuccessCallback?.Invoke();
    }

    private void OnValidate()
    {
        if (_weaponCompositeBehaviour && !(_weaponCompositeBehaviour is IWeaponComposite))
        {
            Debug.LogError(nameof(_weaponCompositeBehaviour) + " needs to implement " + nameof(IWeaponComposite));
            _weaponCompositeBehaviour = null;
        }
    }
}