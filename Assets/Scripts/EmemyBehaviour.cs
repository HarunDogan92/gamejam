using UnityEngine;

public class BoatPatrol : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolRange = 5f;

    [Header("Bomb Drop Settings")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float dropInterval = 2f;
    [SerializeField] private Transform dropPoint;

    private Vector3 startPos;
    private bool movingRight = true;
    private float initialY;
    private float initialZ;

    private float dropTimer;

    private void Start()
    {
        startPos = transform.position;
        initialY = transform.position.y;
        initialZ = transform.position.z;

        dropTimer = dropInterval;
    }

    private void Update()
    {
        Patrol();
        HandleBombDropping();
    }

    private void Patrol()
    {
        float moveStep = patrolSpeed * Time.deltaTime;
        float direction = movingRight ? 1f : -1f;

        transform.position = new Vector3(transform.position.x + direction * moveStep, initialY, initialZ);

        float distanceFromStart = transform.position.x - startPos.x;

        if (movingRight && distanceFromStart >= patrolRange)
        {
            Flip();
        }
        else if (!movingRight && distanceFromStart <= -patrolRange)
        {
            Flip();
        }
    }

    private void HandleBombDropping()
    {
        dropTimer -= Time.deltaTime;
        if (dropTimer <= 0f)
        {
            DropBomb();
            dropTimer = dropInterval;
        }
    }

    private void DropBomb()
    {
        if (bombPrefab != null && dropPoint != null)
        {
            Instantiate(bombPrefab, dropPoint.position, Quaternion.identity);
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
}
