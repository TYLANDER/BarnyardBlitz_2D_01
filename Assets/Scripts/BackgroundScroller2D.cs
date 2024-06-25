using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller2D : MonoBehaviour
{
    public float scrollSpeed = 0.1f;

    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
