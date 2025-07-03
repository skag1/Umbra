using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")] 
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _settingsCanvas;

    [Header("Deactivate Objects with Other Inputs")]
    [SerializeField] private PlayerMovement _playerMovement;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _menuFirst;
    [SerializeField] private GameObject _settingsFirst;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _playerMovement.enabled = true;

        CloseAllMenus();
    }

    private void Update()
    {
        if (InputManager.Instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _playerMovement.enabled = false;

        OpenMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _playerMovement.enabled = true;

        CloseAllMenus();
    }

    #endregion

    #region Canvas Activations

    private void OpenMenu()
    {
        _menuCanvas.SetActive(true);
        _settingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }

    private void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _menuCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsFirst);
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void CloseAllMenus()
    {
        _menuCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    #endregion

    #region Menu Button Actions

    public void OnSettingsPress()
    {
        OpenSettings();
    }

    public void OnContinuePress()
    {
        Unpause();
    }

    public void OnMainMenuPress()
    {
        OpenMainMenu();
    }

    #endregion

    #region Settings Button Actions

    public void OnSettingsBackPress()
    {
        OpenMenu();
    }

    #endregion
}
