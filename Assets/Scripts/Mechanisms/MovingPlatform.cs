using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float waitDuration = 0.15f;

    [Header("Point Targets")]
    [SerializeField] private GameObject targetPoints;
    [SerializeField] private Transform[] points;
    private int pointIndex, pointCount, direction = 1;

    private Rigidbody2D rb;
    private Vector3 targetPos;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerRb;
    private Vector3 moveDirection;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent <Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        pointCount = targetPoints.transform.childCount;
        points = new Transform[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            points[i] = targetPoints.transform.GetChild(i).transform;
        }
    }

    private void Start()
    {
        pointIndex = 1;
        targetPos = points[1].transform.position;
        CalculateDirection();
    }

    private void Update()
    {
        if( Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void NextPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1) //got to last point
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = points[pointIndex].transform.position;

        StartCoroutine(WaitForNextPoint());
    }

    IEnumerator WaitForNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = true;
            playerMovement.platfromRb = rb;
            playerRb.gravityScale = playerRb.gravityScale * 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = false;
            playerRb.gravityScale = playerRb.gravityScale / 2;
        }
    }
}