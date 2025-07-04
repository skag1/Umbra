using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlinkingStaticLight : Light
{
    [Header("Timings")]
    [SerializeField] private float enabledDuration = 2f;
    [SerializeField] private float disabledDuration = 3f;
    [SerializeField] private bool startEnabled = true;

    private Light2D objectLight;
    private Collider2D objectCollider;
    private float timer;
    private bool lightEnabled;

    private void Awake()
    {
        objectLight = GetComponent<Light2D>();
        objectCollider = GetComponent<Collider2D>();

        lightEnabled = startEnabled;
        SetComponentsState(startEnabled);

        // Начинаем с полного времени для текущего состояния
        timer = startEnabled ? enabledDuration : disabledDuration;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Переключаем состояние и устанавливаем соответствующий таймер
            lightEnabled = !lightEnabled;
            SetComponentsState(lightEnabled);
            timer = lightEnabled ? enabledDuration : disabledDuration;
        }
    }

    private void SetComponentsState(bool state)
    {
        if (objectLight != null)
            objectLight.enabled = state;

        if (objectCollider != null)
            objectCollider.enabled = state;
    }
}
