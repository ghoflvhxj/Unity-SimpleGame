using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecter : MonoBehaviour
{
    public enum DetectionType
    {
        Character, Count
    }

    public enum DetectionTeam
    { 
        All, Count
    }

    private bool _detected = false;
    public bool Detected => _detected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(false == other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _detected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (false == other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _detected = false;
    }
}
