using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorHandle : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        Door doorScript = door.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
