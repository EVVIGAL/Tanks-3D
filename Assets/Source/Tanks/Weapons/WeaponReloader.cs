using System;
using System.Collections;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public float ReloadSpeed { get; private set; }
    [field: SerializeField] public uint MagazineSize { get; private set; }

    public bool CanShoot => IsEmpty == false && _isReloading == false;

    public bool IsEmpty => _ammoInMagazine == 0;

    private uint _ammoInMagazine;
    private bool _isReloading;

    private void Awake()
    {
        _ammoInMagazine = MagazineSize;
    }

    public bool TryShoot()
    {
        if (CanShoot == false)
            throw new InvalidOperationException();

        _isReloading = true;
        _ammoInMagazine--;
        Reload();

        return true;
    }

    private void Reload()
    {
        if (IsEmpty)
        {
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
}