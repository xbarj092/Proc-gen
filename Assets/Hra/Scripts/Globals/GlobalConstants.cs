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
        public const string TAG_SHOP = "Shop";
    }

    public class SavedDataPaths
    {
        public class SavedPlayerData
        {
            public static string DATA_PATH_PLAYER_LEVELLING = Application.persistentDataPath + "/levelling.nigger";
            public static string DATA_PATH_PLAYER_TRANSFORM = Application.persistentDataPath + "/transform.nigger";
            public static string DATA_PATH_PLAYER_STATISTICS = Application.persistentDataPath + "/statistics.nigger";
            public static string DATA_PATH_PLAYER_EQUIPPED = Application.persistentDataPath + "/equipped.nigger";
            public static string DATA_PATH_PLAYER_EFFECTS = Application.persistentDataPath + "/effects.nigger";
            public static string DATA_PATH_PLAYER_INVENTORY = Application.persistentDataPath + "/inventory.nigger";
        }

        public class SavedGameData
        {
            public static string DATA_PATH_GAME_MAP = Application.persistentDataPath + "/map.nigger";
        }
    }
}
