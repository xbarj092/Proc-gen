using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    private void Update()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameSceneUIEvents.OnGameScreenOpenedInvoke(GameScreenType.Inventory);
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag(GlobalConstants.Tags.TAG_SHOP))
            {
                GameSceneUIEvents.OnGameScreenOpenedInvoke(GameScreenType.Shop);
            }
        }
    }
}
