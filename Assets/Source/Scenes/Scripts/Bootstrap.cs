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