using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DynamicLight : Light
{
    [SerializeField] protected LayerMask obstacleLayer;
    protected Light2D lightSource;
    protected Transform player;

    private bool wasPlayerInLightLastFrame = false;

    private void Start()
    {
        lightSource = GetComponent<Light2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 direction = player.position - lightSource.transform.position;
        float distance = direction.magnitude;

        // Если игрок вне радиуса света, лучи не достают
        if (distance > lightSource.pointLightOuterRadius)
        {
            wasPlayerInLightLastFrame = false;
            Debug.Log("Игрок вне зоны света");
            return;
        }

        // Проверяем, есть ли препятствие на пути
        RaycastHit2D hit = Physics2D.Raycast(
            lightSource.transform.position,
            direction.normalized,
            distance,
            obstacleLayer
        );

        bool isPlayerInLightNow = (hit.collider == null);

        if (isPlayerInLightNow && !wasPlayerInLightLastFrame)
        {
            PlayerRespawn playerRespawn = player.GetComponent<PlayerRespawn>();
            playerRespawn.Die(true); //fromLight
            Debug.Log("Свет достигает игрока!");
        }

        wasPlayerInLightLastFrame = isPlayerInLightNow;
    }
}
