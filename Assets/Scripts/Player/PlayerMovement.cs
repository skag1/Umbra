using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Platformer Improvment")]
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float maxFallSpeed = -20f;
    private bool isJumping;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip dropSound;

    [Header("Other Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private ParticleSystem smokeFX;


    //Platform movement
    public bool isOnPlatform;
    public Rigidbody2D platfromRb;

    //Animation
    private Animator anim;

    //drop sound fix
    public float dropTime = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        dropTime -= Time.deltaTime; 

        if (horizontal == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            anim.SetBool("isJumping", true);
            smokeFX.Play();
            coyoteTimeCounter -= Time.deltaTime;
            
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            anim.SetTrigger("hasJumped");
            smokeFX.Play();
            SoundFXManager.Instance.PlaySoundFXClip(jumpSound, transform, 0.5f);

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            rb.linearVelocity = new Vector2(horizontal * speed + platfromRb.linearVelocityX, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void HasWon()
    {
        anim.SetTrigger("hasWon");
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            //Vector3 localScale = transform.localScale;
            //isFacingRight = !isFacingRight;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.25f);
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dropTime < 0.001f)
        {
            SoundFXManager.Instance.PlaySoundFXClip(dropSound, transform, 1f);
            dropTime = 0.25f;
        }
    }
}