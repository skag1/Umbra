using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeSelector : MonoBehaviour
{
    public Image[] volumeBlocks; // 5 �����������-���������
    public int currentVolume = 3; // ������� ������� ��������� (0-5)
    public GameObject focusTarget; // ������, ������� ������ ���� � ������ ��� ����������

    void Start()
    {
        UpdateVisuals();
    }

    void Update()
    {
        // �������� � � ������ �� ������ ������?
        if (EventSystem.current.currentSelectedGameObject != focusTarget)
            return;

        // ���������� �����
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentVolume = Mathf.Max(0, currentVolume - 1);
            UpdateVisuals();
        }

        // ���������� ������
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentVolume = Mathf.Min(volumeBlocks.Length, currentVolume + 1);
            UpdateVisuals();
        }
    }

    void UpdateVisuals()
    {
        for (int i = 0; i < volumeBlocks.Length; i++)
        {
            volumeBlocks[i].color = i < currentVolume ? Color.green : Color.gray;
        }
    }
}