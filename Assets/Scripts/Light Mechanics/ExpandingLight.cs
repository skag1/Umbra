using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExpandingLight : DynamicLight
{
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 8f;
    [SerializeField] private float speed = 1f;
    private float currentRadius;
    private bool isGrowing = true;

    private void Start()
    {
        lightSource = GetComponent<Light2D>();
        player = GameObject.FindWithTag("Player").transform;
        currentRadius = minRadius;
    }

    void Update()
    {
        UpdateLightRadius();
        CheckPlayerInLight();
    }

    void UpdateLightRadius()
    {
        if (isGrowing)
        {
            currentRadius = Mathf.MoveTowards(currentRadius, maxRadius, speed * Time.deltaTime);
            if (currentRadius >= maxRadius) isGrowing = false;
        }
        else
        {
            currentRadius = Mathf.MoveTowards(currentRadius, minRadius, speed * Time.deltaTime);
            if (currentRadius <= minRadius) isGrowing = true;
        }

        lightSource.pointLightOuterRadius = currentRadius;
    }

    void CheckPlayerInLight()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > currentRadius) return;

        Vector2 direction = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction.normalized,
            distance,
            obstacleLayer
        );

        if (hit.collider == null)
        {
            player.GetComponent<PlayerRespawn>().Die(true);
            Debug.Log("Игрок убит светом (радиус: " + currentRadius + ")");
        }
    }
}
