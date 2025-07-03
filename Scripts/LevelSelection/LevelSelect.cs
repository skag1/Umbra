using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    public Transform LevelParent;
    public GameObject LevelButtonPrefab;
    public TextMeshProUGUI AreaHeaderText;
    public TextMeshProUGUI LevelHeaderText;
    public AreaData CurrentArea;

    public HashSet<string> UnlockedLevelIDs = new HashSet<string>();

    private LevelSelectEventSystemHandler _eventSystemHandler;

    private Camera _camera;

    private List<GameObject> _buttonObjects = new List<GameObject>();
    private Dictionary<GameObject, Vector3> _buttonLocations = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _eventSystemHandler = GetComponentInChildren<LevelSelectEventSystemHandler>(true);
    }

    private void Start()
    {
        AssignAreaText();
        LoadUnlockedLevels();
        CreateLevelButtons();
    }

    public void AssignAreaText()
    {
        AreaHeaderText.SetText(CurrentArea.AreaName);
    }

    private void LoadUnlockedLevels()
    {
        foreach (var level in CurrentArea.Levels)
        {
            if (level.IsUnlockedByDefault)
            {
                UnlockedLevelIDs.Add(level.LevelID);
            }
        }
    }

    private void CreateLevelButtons()
    {
        for (int i = 0; i < CurrentArea.Levels.Count; i++)
        {
            GameObject buttonGO = Instantiate(LevelButtonPrefab, LevelParent);
            _buttonObjects.Add(buttonGO);

            RectTransform buttonReact = buttonGO.GetComponent<RectTransform>();

            buttonGO.name = CurrentArea.Levels[i].LevelID;
            CurrentArea.Levels[i].LevelButtonObj = buttonGO;

            LevelButton levelButton = buttonGO.GetComponent<LevelButton>();
            levelButton.Setup(CurrentArea.Levels[i], UnlockedLevelIDs.Contains(CurrentArea.Levels[i].LevelID));

            Selectable selectable = buttonGO.GetComponent<Selectable>();
            _eventSystemHandler.AddSelectable(selectable);
        }

        LevelParent.gameObject.SetActive(true);
        _eventSystemHandler.InitSelectables();
        _eventSystemHandler.SetFirstSelected();
    }

    private void OnEnable()
    {
        _eventSystemHandler.SetFirstSelected();
    }

    #region Navigation

    private IEnumerator SetupButtonNavigation()
    {
        yield return null;

        for (int i = 0; i < _buttonObjects.Count; i++)
        {
            GameObject currentButton = _buttonObjects[i];
            Vector3 currentPos = _buttonLocations[currentButton];
            Selectable currentSelectable = currentButton.GetComponent<Selectable>();
            Navigation nav = new Navigation { mode = Navigation.Mode.Explicit };

            // Check if previous button exists
            if (i > 0 && UnlockedLevelIDs.Contains(CurrentArea.Levels[i].LevelID))
            {
                GameObject prevButton = _buttonObjects[i - 1];
                Vector3 prevPos = _buttonLocations[prevButton];
                Vector3 dirToPrev = (prevPos - currentPos).normalized;

                if (Vector3.Dot(dirToPrev, Vector3.right) > 0.7f)
                    nav.selectOnRight = prevButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToPrev, Vector3.left) > 0.7f)
                    nav.selectOnLeft = prevButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToPrev, Vector3.up) > 0.7f)
                    nav.selectOnUp = prevButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToPrev, Vector3.down) > 0.7f)
                    nav.selectOnDown = prevButton.GetComponent<Selectable>();
            }

            // Check if next button exists
            if (i < _buttonObjects.Count - 1 && UnlockedLevelIDs.Contains(CurrentArea.Levels[i + 1].LevelID))
            {
                GameObject nextButton = _buttonObjects[i + 1];
                Vector3 nextPos = _buttonLocations[nextButton];
                Vector3 dirToNext = (nextPos - currentPos).normalized;

                if (Vector3.Dot(dirToNext, Vector3.right) > 0.7f)
                    nav.selectOnRight = nextButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToNext, Vector3.left) > 0.7f)
                    nav.selectOnLeft = nextButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToNext, Vector3.up) > 0.7f)
                    nav.selectOnUp = nextButton.GetComponent<Selectable>();
                else if (Vector3.Dot(dirToNext, Vector3.down) > 0.7f)
                    nav.selectOnDown = nextButton.GetComponent<Selectable>();
            }

            currentSelectable.navigation = nav;
        }
    }

    #endregion

    #region Helper Methods

    public void UnLockLevel(string levelID, LevelButton levelButton)
    {
        UnlockedLevelIDs.Add(levelID);
        levelButton.Unlock();
        StartCoroutine(SetupButtonNavigation());
    }

    [ContextMenu("Test Level2 UnLock")]
    public void UnLockLevelTwoExample()
    {
        LevelButton levelButton = _buttonObjects[1].GetComponent<LevelButton>();
        string levelToUnlock = levelButton.LevelData.LevelID;
        UnLockLevel(levelToUnlock, levelButton);
    }

    #endregion
}
