using UnityEngine;

/// <summary>
/// Contains global constants used throughout the application.
/// </summary>
public static class GlobalConstants
{
    /// <summary>
    /// Contains tag constants used to identify game objects.
    /// </summary>
    public class Tags
    {
        public const string TAG_ROOM = "Room";
        public const string TAG_HALLWAY = "Hallway";
    }

    public class SavedDataPaths
    {
        public class SavedPlayerData
        {
            public static string DATA_PATH_PLAYER_LEVELLING = Application.persistentDataPath + "/levelling.nigger";
            public static string DATA_PATH_PLAYER_TRANSFORM = Application.persistentDataPath + "/transform.nigger";
        }

        public class SavedGameData
        {
            public static string DATA_PATH_GAME_MAP = Application.persistentDataPath + "/map.nigger";
        }
    }
}
