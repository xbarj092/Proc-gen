using System.Collections.Generic;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] private InventoryScreen _inventoryScreenPrefab;
    [SerializeField] private ItemInfoScreen _itemInfoScreenPrefab;
    [SerializeField] private ShopScreen _shopScreenPrefab;

    private Dictionary<GameScreenType, GameScreen> _instantiatedScreens = new();

    private void OnEnable()
    {
        GameSceneUIEvents.OnGameScreenOpened += ShowGameScreen;
        GameSceneUIEvents.OnGameScreenClosed += CloseGameScreen;
    }

    private void OnDisable()
    {
        GameSceneUIEvents.OnGameScreenOpened -= ShowGameScreen;
        GameSceneUIEvents.OnGameScreenClosed -= CloseGameScreen;
    }

    private void ShowGameScreen(GameScreenType gameScreenType)
    {
        if ((_instantiatedScreens.ContainsKey(gameScreenType) && _instantiatedScreens[gameScreenType] == null) || 
            !_instantiatedScreens.ContainsKey(gameScreenType))
        {
            InstantiateScreen(gameScreenType);
        }

        _instantiatedScreens[gameScreenType].Open();
    }

    private void CloseGameScreen(GameScreenType gameScreenType)
    {
        if (_instantiatedScreens.ContainsKey(gameScreenType))
        {
            _instantiatedScreens[gameScreenType].Close();
            _instantiatedScreens.Remove(gameScreenType);
        }
    }

    private void InstantiateScreen(GameScreenType gameScreenType)
    {
        GameScreen screenInstance = GetRelevantScreen(gameScreenType);
        if (screenInstance != null)
        {
            _instantiatedScreens[gameScreenType] = screenInstance;
        }
    }

    private GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Inventory => Instantiate(_inventoryScreenPrefab, transform),
            GameScreenType.ItemInfo => Instantiate(_itemInfoScreenPrefab, transform),
            GameScreenType.Shop => Instantiate(_shopScreenPrefab, transform),
            _ => null,
        };
    }
}
