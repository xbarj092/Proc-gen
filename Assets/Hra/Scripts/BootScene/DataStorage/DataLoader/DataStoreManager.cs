using EasyButtons;

public class DataStoreManager : MonoSingleton<DataStoreManager>
{
    private DataSaver _dataSaver = new();
    private DataLoader _dataLoader = new();

    [Button]
    public void SaveData()
    {
        SavePlayerData();
        SaveGameData();
    }
    
    private void SavePlayerData()
    {
        PlayerData playerData = LocalDataStorage.Instance.PlayerData;

        _dataSaver.SaveData(playerData.TransformData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_TRANSFORM);
        _dataSaver.SaveData(playerData.LevellingData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_LEVELLING);
    }

    private void SaveGameData()
    {
        GameData gameData = LocalDataStorage.Instance.GameData;

        _dataSaver.SaveData(gameData.MapTransformData, GlobalConstants.SavedDataPaths.SavedGameData.DATA_PATH_GAME_MAP);
    }

    [Button]
    public void LoadData()
    {
        LoadPlayerData();
        LoadGameData();
    }

    private void LoadPlayerData()
    {
        PlayerData playerData = LocalDataStorage.Instance.PlayerData;

        playerData.TransformData = _dataLoader.LoadData<TransformData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_TRANSFORM);
        playerData.LevellingData = _dataLoader.LoadData<LevellingData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_LEVELLING);
    }

    private void LoadGameData()
    {
        GameData gameData = LocalDataStorage.Instance.GameData;

        gameData.MapTransformData = _dataLoader.LoadData<MapTransformData>(GlobalConstants.SavedDataPaths.SavedGameData.DATA_PATH_GAME_MAP);
    }
}
