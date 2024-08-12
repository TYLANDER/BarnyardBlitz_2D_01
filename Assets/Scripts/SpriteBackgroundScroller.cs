using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float spriteHeight = 18.0f; // Set this to the height of your sprite in world units
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, spriteHeight);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
