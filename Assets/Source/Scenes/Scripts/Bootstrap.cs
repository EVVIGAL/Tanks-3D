using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace CrazyGames.Tanks3D
{
    public class Bootstrap : MonoBehaviour
    {
        private int _startSceneIndex = 1;

        private void Start()
        {
            if (YG2.isSDKEnabled)
                StartGame();
            else
                YG2.onGetSDKData += OnGetSDKData;
        }

        private void StartGame()
        {
            string languageCode = string.IsNullOrEmpty(YG2.saves.Language) ? YG2.envir.language : YG2.saves.Language;
            if (RuntimeConstants.Language.ISO639_1Codes.TryGetValue(languageCode, out string language) == false)
                language = "English";

            LeanLocalization.SetCurrentLanguageAll(language);

            if (YG2.player.auth == false)
                YG2.player.name = LeanLocalization.GetTranslationText("Guest", "Guest");

            SceneManager.LoadScene(_startSceneIndex);
        }

        private void OnGetSDKData()
        {
            StartGame();
        }
    }
}