using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBreaker : MonoBehaviour
{
    private GameManager gameManager;

    private Dictionary<int, GameObject> targetMap = new Dictionary<int, GameObject>();
    private float collectTime = 1.0f;
    private bool collectTarget = true;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collectTime - Time.deltaTime < 0.0f)
        {
            collectTarget = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(false == collectTarget)
        {
            return;
        }

        if(true == other.gameObject.CompareTag("Actor"))
        {
            int key = other.gameObject.GetHashCode();
            if (true == targetMap.ContainsKey(key))
            {
                return;
            }

            targetMap.Add(key, other.gameObject);

            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce((transform.position - other.transform.position) * 3.0f, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        int key = other.gameObject.GetHashCode();
        if(false == targetMap.ContainsKey(key))
        {
            return;
        }

        if (Vector3.Distance(transform.position, other.transform.position) < 1.0f)
        {
            return;
        }

        //Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        //rigidbody.AddForce((transform.position - other.transform.position), ForceMode.Impulse);
    
    }
}
