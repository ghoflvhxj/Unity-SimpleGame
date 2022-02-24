using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어만 사용
        if(false == collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

        // 낙하 할때만 트램펄린 동작
        if(rigidbody.velocity.y > 0.0f)
        {
            return;
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * 20.0f, ForceMode.VelocityChange);
    }
}
