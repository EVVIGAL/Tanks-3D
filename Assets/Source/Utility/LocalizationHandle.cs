using CrazyGames.Tanks3D;
using Lean.Localization;
using UnityEngine;
using YG;

public class LocalizationHandle : MonoBehaviour
{
    private void Awake()
    {
        string languageCode = string.IsNullOrEmpty(YG2.saves.Language) ? YG2.envir.language : YG2.saves.Language;
        if (RuntimeConstants.Language.ISO639_1Codes.TryGetValue(languageCode, out string language) == false)
            language = "English";

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}