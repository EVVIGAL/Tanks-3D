using System;
using System.Collections;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public float ReloadSpeed { get; private set; }
    [field: SerializeField] public uint MagazineSize { get; private set; }

    [SerializeField] private WeaponReloaderView _weaponReloaderView;

    private uint _ammoInMagazine;
    private bool _isReloading;
    private float _runningTime;

    public bool CanShoot => IsEmpty == false && _isReloading == false;

    public bool IsEmpty => _ammoInMagazine == 0;

    public void Init(WeaponReloaderView shootButtonView)
    {
        _weaponReloaderView = shootButtonView ?? throw new ArgumentNullException();
    }

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
            StartCoroutine(Reloading());
        else
            StartCoroutine(Wait(RateOfFire, () => _isReloading = false));
    }

    private IEnumerator Wait(float time, Action onSuccessCallback)
    {
        yield return new WaitForSeconds(time);
        onSuccessCallback?.Invoke();
    }

    private IEnumerator Reloading()
    {
        _runningTime = 0f;
        while (_runningTime < ReloadSpeed)
        {
            _runningTime += Time.deltaTime;

            if (_weaponReloaderView)
                _weaponReloaderView.ShowReloadProgress(_runningTime / ReloadSpeed);

            yield return null;
        }

        _ammoInMagazine = MagazineSize;
        _isReloading = false;
    }
}