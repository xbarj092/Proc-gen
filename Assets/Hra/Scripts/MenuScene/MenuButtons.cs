using System;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public event Action OnOptionScreenClicked;

    // bound from inspector
    public void StartGame()
    {
        SceneLoadManager.Instance.GoMenuToGame();
    }

    // bound from inspector
    public void ExitGame()
    {
        Application.Quit();
    }

    // bound from inspector
    public void GoToOptions()
    {
        OnOptionScreenClicked?.Invoke();
    }
}
