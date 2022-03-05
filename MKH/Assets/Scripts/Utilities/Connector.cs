using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Connector))]
class ConnectionDrawScipt : Editor
{
    void OnSceneGUI()
    {
        Connector connector = target as Connector;
        if (null == connector.linkedObjects)
        {
            return;
        }

        Vector3 center = connector.transform.position;
        foreach(GameObject connectedObject in connector.linkedObjects)
        {
            Handles.DrawLine(center, connectedObject.transform.position);
        }
    }
}


public class Connector : MonoBehaviour
{
    public GameObject[] linkedObjects = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
