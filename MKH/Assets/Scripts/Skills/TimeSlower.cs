using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlower : MonoBehaviour
{
    [SerializeField]
    private float slowScale = 10.0f;
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private float slowRange = 3.0f;

    private SphereCollider sphereCollider;

    struct RigidbodyInfo
    {
        public Vector3 originVelocity;
        public Vector3 addtiveVelocity;
    }

    Dictionary<int, RigidbodyInfo> collideRigidbodynfoMap = new Dictionary<int, RigidbodyInfo>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = slowRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (true == other.gameObject.CompareTag("Actor"))
        {
            int key = other.GetHashCode();
            if (false == collideRigidbodynfoMap.ContainsKey(key))
            {
                Rigidbody collideRigidbody= other.gameObject.GetComponent<Rigidbody>();

                RigidbodyInfo info;
                info.originVelocity = collideRigidbody.velocity;
                info.addtiveVelocity = Vector3.zero;
                collideRigidbodynfoMap.Add(key, info);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (true == other.gameObject.CompareTag("Actor"))
        {
            int key = other.GetHashCode();
            if(false == collideRigidbodynfoMap.ContainsKey(key))
            {
                return;
            }

            Vector3 newVelocity = (collideRigidbodynfoMap[key].originVelocity / slowScale) + (collideRigidbodynfoMap[key].addtiveVelocity / slowScale);

            Rigidbody collideRigidbody = other.gameObject.GetComponent<Rigidbody>();
            collideRigidbody.velocity = (collideRigidbodynfoMap[key].originVelocity / slowScale) + (collideRigidbodynfoMap[key].addtiveVelocity / slowScale);
            collideRigidbody.angularVelocity = Vector3.zero;

            RigidbodyInfo collideRigidbodynfo = collideRigidbodynfoMap[key];
            collideRigidbodynfo.addtiveVelocity = (collideRigidbody.velocity * slowScale) - collideRigidbodynfoMap[key].originVelocity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(true == other.gameObject.CompareTag("Actor"))
        {
            int key = other.GetHashCode();
            if(true == collideRigidbodynfoMap.ContainsKey(key))
            {
                Rigidbody collideRigidbody= other.gameObject.GetComponent<Rigidbody>();
                collideRigidbody.velocity = collideRigidbodynfoMap[key].originVelocity + collideRigidbodynfoMap[key].addtiveVelocity;
            }
        }
    }
}
