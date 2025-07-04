using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private GameObject playerDeath;
    [SerializeField] private GameObject particleEffect;
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

        if (collision.CompareTag("DeathBox"))
        {
            Die(false);
        }
    }


    public void Die(bool fromLight)
    {
        if (fromLight)
        {
            SoundFXManager.Instance.PlaySoundFXClip(damageSound, transform, 2f);
        }
        StartCoroutine(Respawn(respawnDelay, fromLight));
    }

    IEnumerator Respawn(float delay, bool fromLight)
    {
        if (!fromLight)
        {
            sprite.enabled = false;
            transform.position = new Vector3(-1000, -1000, -1000);
            SoundFXManager.Instance.PlaySoundFXClip(deathSound, transform, 1f);
            yield return new WaitForSeconds(delay);
            transform.position = startPos;
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;

            GameObject deathAnim = Instantiate(playerDeath, transform.position, Quaternion.identity);
            GameObject particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
            particle.gameObject.GetComponent<ParticleFollower>().SetTarget(deathAnim.transform);
            Rigidbody2D rbDeath = deathAnim.GetComponent<Rigidbody2D>();
            Animator animator = deathAnim.GetComponent<Animator>();
            animator.enabled = false;

            if (rbDeath != null)
            {
                rbDeath.bodyType = RigidbodyType2D.Kinematic;
            }
            transform.position = new Vector3(-1000, -1000, -1000);


            yield return new WaitForSeconds(0.6f);
            animator.enabled = true;
            SoundFXManager.Instance.PlaySoundFXClip(deathSound, transform, 1f);
            if (rbDeath != null)
            {
                rbDeath.bodyType = RigidbodyType2D.Dynamic;
            }


            yield return new WaitForSeconds(delay);
            Destroy(deathAnim);
            transform.position = startPos;
            sprite.enabled = true;
        }
    }

}
