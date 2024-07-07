using UnityEngine;

public enum GameScreenType
{
    None = 0,
    Shop = 1,
    Inventory = 2,
    ItemInfo = 3,
}

public class GameScreen : MonoBehaviour
{
    [SerializeField] private bool _destroyOnClose;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        if (_destroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
