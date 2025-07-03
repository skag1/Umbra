using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DynamicLight : MonoBehaviour
{
    [SerializeField] protected LayerMask obstacleLayer;
    protected Light2D lightSource;
    protected Transform player;

    private void Start()
    {
        lightSource = GetComponent<Light2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 direction = player.position - lightSource.transform.position;
        float distance = direction.magnitude;

        // ���� ����� ��� ������� �����, ���� �� �������
        if (distance > lightSource.pointLightOuterRadius)
        {
            Debug.Log("����� ��� ���� �����");
            return;
        }

        // ���������, ���� �� ����������� �� ����
        RaycastHit2D hit = Physics2D.Raycast(
            lightSource.transform.position,
            direction.normalized,
            distance,
            obstacleLayer
        );

        if (hit.collider != null)
        {
            Debug.Log("����������� ��������� ����: " + hit.collider.name);
        }
        else
        {
            PlayerRespawn playerRespawn = player.GetComponent<PlayerRespawn>();
            playerRespawn.Die(true); //fromLight
            Debug.Log("���� ��������� ������!");
        }
    }
}
