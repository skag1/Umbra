using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectEventSystemHandler : DynamicEventSystemHandler
{
    private Image _image;
    private LevelButton _levelButton;
    private LevelSelect _levelSelectManager;

    private void Awake()
    {
        _levelSelectManager = GetComponentInParent<LevelSelect>();
    }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    public override void OnSelect(BaseEventData eventData)
    {
        Debug.Log("кнопка выбрана");
        base.OnSelect(eventData);

        _image = eventData.selectedObject.GetComponent<Image>();
        _levelButton = eventData.selectedObject.GetComponent<LevelButton>();
        if (_levelButton != null)
        {
            Debug.Log("кнопка существует");
            _levelSelectManager.LevelHeaderText.SetText(_levelButton.LevelData.LevelName);

            if (_image != null)
            {
                Debug.Log("картинка существует");
                _image.color = Color.green;
            }
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        if (_levelButton != null)
        {
            _levelSelectManager.LevelHeaderText.SetText("");

            if (_image != null)
            {
                _image.color = _levelButton.ReturnColor;
            }
        }
    }
}
