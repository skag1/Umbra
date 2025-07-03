using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 0.5f;
    private Vector2 startPos;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StaticLight"))
        {
            Die(true);
        }
    }

    public void Die(bool fromLight)
    {
        StartCoroutine(Respawn(respawnDelay, fromLight));
    }

    IEnumerator Respawn(float delay, bool fromLight)
    {
        sprite.enabled = false;
        //transform.position = new Vector3(-1000, -1000, -1000);
        yield return new WaitForSeconds(delay);
        transform.position = startPos;
        sprite.enabled = true;
    }
}
