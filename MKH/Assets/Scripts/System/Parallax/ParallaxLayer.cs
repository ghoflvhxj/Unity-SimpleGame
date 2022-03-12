using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField]
    private float parallaxFactor = 1f;

    public float a;

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;

        a = delta * parallaxFactor;

        transform.localPosition = newPos;
    }
}
