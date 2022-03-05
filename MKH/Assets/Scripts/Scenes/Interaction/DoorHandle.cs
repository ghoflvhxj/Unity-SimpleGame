using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorHandle : Interactable
{
    private Connector connector;
    // Start is called before the first frame update
    void Start()
    {
        connector = GetComponent<Connector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInteract()
    {
        base.OnInteract();

        foreach(GameObject gameObject in connector.linkedObjects)
        {
            Door door = gameObject.GetComponent<Door>();
            if(null == door)
            {
                Debug.Log("Door °´Ã¼°¡ ¾Æ´Õ´Ï´Ù.");
            }

            door.Open();
        }

    }
}
