using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScene : MonoBehaviour
{
    public ParallaxCamera parallaxCamera = null;
    private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    // Start is called before the first frame update
    void Start()
    {
        if(null != parallaxCamera)
        {
            parallaxCamera.cameraMoveDelegate += Move;
        }

        parallaxLayers.Clear();
        for(int i=0; i<transform.childCount; ++i)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
            if(layer == null)
            {
                Debug.LogError(transform.GetChild(i).gameObject.name + "에 Parallax 스크립트가 적용이 안되있습니다.");
                continue;
            }

            parallaxLayers.Add(layer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(float delta)
    {
        foreach(ParallaxLayer parallaxLayer in parallaxLayers)
        {
            parallaxLayer.Move(delta);
        }
    }
}
