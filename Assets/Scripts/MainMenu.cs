using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _levelsCanvas;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsFirst;
    [SerializeField] private GameObject _levelsFirst;

    private void Start()
    {
        OpenMenu();
    }

    #region Canvas Activations

    private void OpenMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsCanvas.SetActive(false);
        _levelsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    private void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
        _levelsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsFirst);
    }

    private void OpenLevels()
    {
        _levelsCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_levelsFirst);
    }

    #endregion

    #region Main Menu Button Actions

    public void OnSettingsPress()
    {
        OpenSettings();
    }

    public void OnNewGamePress()
    {
        SceneManager.LoadScene("1");
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnLevelsPress()
    {
        OpenLevels();
    }

    #endregion

    #region Settings Button Actions

    public void OnSettingsBackPress()
    {
        OpenMenu();
    }

    #endregion
}