using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            camera.aspect = 16f / 9f;
        }
    }
}
