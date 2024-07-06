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

    [Button]
    public void LoadData()
    {
        LoadPlayerData();
        LoadGameData();
    }

    private void SavePlayerData()
    {
        PlayerData playerData = LocalDataStorage.Instance.PlayerData;

        _dataSaver.SaveData(playerData.TransformData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_TRANSFORM);
        _dataSaver.SaveData(playerData.LevellingData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_LEVELLING);
        _dataSaver.SaveData(playerData.PlayerStatistics, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_STATISTICS);
        _dataSaver.SaveData(playerData.EquippedItemsData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_EQUIPPED);
        _dataSaver.SaveData(playerData.SpecialEffectsData, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_EFFECTS);
        _dataSaver.SaveData(playerData.InventoryItems, GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_INVENTORY);
    }

    private void LoadPlayerData()
    {
        PlayerData playerData = LocalDataStorage.Instance.PlayerData;

        playerData.TransformData = _dataLoader.LoadData<TransformData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_TRANSFORM);
        playerData.LevellingData = _dataLoader.LoadData<LevellingData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_LEVELLING);
        playerData.PlayerStatistics = _dataLoader.LoadData<PlayerStatistics>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_STATISTICS);
        playerData.EquippedItemsData = _dataLoader.LoadData<EquippedItemsData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_EQUIPPED);
        playerData.SpecialEffectsData = _dataLoader.LoadData<SpecialEffectsData>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_EFFECTS);
        playerData.InventoryItems = _dataLoader.LoadData<InventoryItems>(GlobalConstants.SavedDataPaths.SavedPlayerData.DATA_PATH_PLAYER_INVENTORY);
    }

    private void SaveGameData()
    {
        GameData gameData = LocalDataStorage.Instance.GameData;

        _dataSaver.SaveData(gameData.MapTransformData, GlobalConstants.SavedDataPaths.SavedGameData.DATA_PATH_GAME_MAP);
    }

    private void LoadGameData()
    {
        GameData gameData = LocalDataStorage.Instance.GameData;

        gameData.MapTransformData = _dataLoader.LoadData<MapTransformData>(GlobalConstants.SavedDataPaths.SavedGameData.DATA_PATH_GAME_MAP);
    }
}
