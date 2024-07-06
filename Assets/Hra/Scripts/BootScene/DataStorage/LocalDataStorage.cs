using UnityEngine;

public class LocalDataStorage : MonoSingleton<LocalDataStorage>
{
    [field: SerializeField] public PlayerData PlayerData;
    [field: SerializeField] public GameData GameData;
    [field: SerializeField] public TitleData TitleData;
    [field: SerializeField] public CatalogItems CatalogItems = new();

    private void Awake()
    {
        TitleData.SetUpTitleData();
        CatalogItems.InitializeCatalog();
    }
}
