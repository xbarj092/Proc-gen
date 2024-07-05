using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    [SerializeField] private MenuButtons _menuButtonsPrefab;
    [SerializeField] private OptionScreen _optionScreenPrefab;

    private MenuButtons _menuButtonsInstantiated;
    private OptionScreen _optionScreenInstantiated;

    private void Awake()
    {
        ShowMenuButtons();
    }

    private void OnDisable()
    {
        if ( _menuButtonsInstantiated != null )
        {
            _menuButtonsInstantiated.OnOptionScreenClicked -= ShowOptionScreen;
        }
    }

    private void ShowMenuButtons()
    {
        if (_menuButtonsInstantiated == null)
        {
            _menuButtonsInstantiated = Instantiate(_menuButtonsPrefab, transform);
            _menuButtonsInstantiated.OnOptionScreenClicked += ShowOptionScreen;
        }
        else
        {
            _menuButtonsInstantiated.gameObject.SetActive(true);
        }
    }

    private void ShowOptionScreen()
    {
        if (_optionScreenInstantiated == null)
        {
            _optionScreenInstantiated = Instantiate(_optionScreenPrefab, transform);
        }
        else
        {
            _optionScreenInstantiated.gameObject.SetActive(true);
        }
    }
}
