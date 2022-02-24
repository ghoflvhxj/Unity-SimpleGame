using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private BoxCollider boxColiider = null;
    public delegate void interaction();
    public interaction a;

    // Start is called before the first frame update
    void Start()
    {
        boxColiider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(false == other.CompareTag("Player"))
        {
            return;
        }

        if(true == Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}
