using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // This function is called by the animation event
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
