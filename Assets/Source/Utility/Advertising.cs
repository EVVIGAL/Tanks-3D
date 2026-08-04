using CrazyGames.Tanks3D;
using System;
using YG;

public static class Advertising
{
    private static Action _endCallback;

    public static void AddListener()
    {
        YG2.onCloseInterAdv += OnCloseInterAdv;
        YG2.onErrorInterAdv += OnErrorInterAdv;
    }

    public static void RemoveListener()
    {
        YG2.onCloseInterAdv -= OnCloseInterAdv;
        YG2.onErrorInterAdv -= OnErrorInterAdv;
    }

    public static void ShowAd(Action endCallback = null)
    {
        _endCallback = endCallback;

        if (YG2.platform == RuntimeConstants.Platforms.YandexGames)
        {
            if (YG2.isTimerAdvCompleted)
                YG2.InterstitialAdvShow();
            else
                _endCallback?.Invoke();
        }
        else
        {
            YG2.InterstitialAdvShow();
        }
    }

    private static void OnErrorInterAdv()
    {
        _endCallback?.Invoke();
    }

    private static void OnCloseInterAdv()
    {
        _endCallback?.Invoke();
    }
}