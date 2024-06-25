using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller2D : MonoBehaviour
{
    // Speed at which the background scrolls
    public float scrollSpeed = 0.1f;

    void Update()
    {
        // Calculate the offset based on time and scroll speed
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        // Apply the offset to the material to create a scrolling effect
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
