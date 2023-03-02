using UnityEngine;
using UnityEngine.UI;

public class WeaponReloaderView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _reloadProgressBar;

    public void ShowReloadProgress(float reloadTime)
    {
        _reloadProgressBar.fillAmount = reloadTime;
        bool active = Mathf.Approximately(_reloadProgressBar.fillAmount, 1f);
        _button.interactable = active;
        _reloadProgressBar.gameObject.SetActive(!active);
    }
}