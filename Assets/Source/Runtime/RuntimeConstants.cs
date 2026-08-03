using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace CrazyGames.Tanks3D
{
    public static class RuntimeConstants
    {
        public readonly static int RandomMapId = 0;
        public readonly static int AppKey = 18071989;

        public const string DataBasePath = "DataBase";
        public const string GameConfigPath = "GameConfig";

        public const int MinPlayersStartBattle = 2;

        public static class Photon
        {
            public const string Session = nameof(Session);
            public const string AppVersion = nameof(AppVersion);
            public const string Region = nameof(Region);
            public const string InviteId = nameof(InviteId);
        }

        public static class Session
        {
            public const string NewSession = nameof(NewSession);
            public const string BattleMode = nameof(BattleMode);
            public const string IsRandomBattle = nameof(IsRandomBattle);
            public const string SessionName = nameof(SessionName);
            public const string MapId = nameof(MapId);
        }

        public static class Scenes
        {
            public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("Bootstrap");
            public static readonly int Hangar = SceneUtility.GetBuildIndexByScenePath("Hangar");
        }

        public static class Platforms
        {
            public const string YandexGames = "YandexGames";
            public const string CrazyGames = "CrazyGames";
            public const string GameMonetize = "GameMonetize";
        }

        public static class Language
        {
            public static Dictionary<string, string> ISO639_1Codes = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "ar", "Arabic" },
        { "de", "German" },
        { "es", "Spanish" },
        { "tr", "Turkish" },
    };
        }

        public static class Audio
        {
            public const string MainAudioMixer = nameof(MainAudioMixer);
            public const string MusicVolume = nameof(MusicVolume);
            public const string EffectsVolume = nameof(EffectsVolume);
        }
    }
}