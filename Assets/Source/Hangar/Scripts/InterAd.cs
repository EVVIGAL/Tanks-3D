using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using YG;

[RequireComponent(typeof(Button))]
public class InterAd : MonoBehaviour
{
    private UnityAction _onCloseAdvCallback;

    private void OnEnable()
    {
        YG2.onCloseInterAdv += OnCloseInterAdv;
        YG2.onErrorInterAdv += OnErrorInterAdv;
    }

    private void OnDisable()
    {
        YG2.onCloseInterAdv -= OnCloseInterAdv;
        YG2.onErrorInterAdv -= OnErrorInterAdv;
    }

    public void ShowAD(UnityAction action)
    {
        _onCloseAdvCallback = action;
        YG2.InterstitialAdvShow();
    }

    private void OnErrorInterAdv()
    {
        _onCloseAdvCallback?.Invoke();
    }

    private void OnCloseInterAdv()
    {
        _onCloseAdvCallback?.Invoke();
    }
}