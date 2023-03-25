using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;

[RequireComponent(typeof(LeanLocalization))]
public class Localization : MonoBehaviour
{
    private LeanLocalization _localization;
    private const string _defaultKey = "en";

    private Dictionary<string, string> _languageISO639_1Codes = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "tr", "Turkish" },
    };

    private void Awake()
    {
        _localization = GetComponent<LeanLocalization>();
    }

    private void OnEnable()
    {
        Set();
    }

    public void Set()
    {
        _localization.SetCurrentLanguage(_languageISO639_1Codes[_defaultKey]);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (Yandex.Instance == null)
            return;

        if (_languageISO639_1Codes.ContainsKey(Yandex.Instance.CurrentLanguage))
            _localization.SetCurrentLanguage(_languageISO639_1Codes[Yandex.Instance.CurrentLanguage]);
        else
            _localization.SetCurrentLanguage(_languageISO639_1Codes[_defaultKey]);
#endif
    }
}