using UnityEngine;

public class BoatPatrol : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolRange = 5f;

    private Vector3 startPos;
    private bool movingRight = true;
    private float initialY;
    private float initialZ;

    private void Start()
    {
        startPos = transform.position;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        float moveStep = patrolSpeed * Time.deltaTime;
        float direction = movingRight ? 1f : -1f;

        // Only move on X, keep Y and Z fixed
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

    private void Flip()
    {
        movingRight = !movingRight;

        // Mirror the sprite by flipping localScale.x
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
}
