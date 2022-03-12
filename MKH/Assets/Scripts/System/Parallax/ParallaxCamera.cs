using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate cameraMoveDelegate;
    private float oldPositionX;

    void Start()
    {
        oldPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == oldPositionX)
        {
            return;
        }

        if (null == cameraMoveDelegate)
        {
            return;
        }

        float delta = oldPositionX - transform.position.x;
        cameraMoveDelegate(delta);

        oldPositionX = transform.position.x;
    }
}
