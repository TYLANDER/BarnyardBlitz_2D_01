using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private BoxCollider2D boundaryCollider;

    void Start()
    {
        // Find the boundary collider in the scene
        boundaryCollider = GameObject.Find("Bounds").GetComponent<BoxCollider2D>();

        // Calculate the bounds based on the collider size and position
        minBounds = boundaryCollider.bounds.min;
        maxBounds = boundaryCollider.bounds.max;
    }

    void Update()
    {
        // Clamp the player's position to ensure it stays within the bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z
        );
        transform.position = clampedPosition;
    }
}
