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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어만 사용
        if(false == collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if(null == rigidbody)
        {
            Debug.LogError("플레이어인데 Rigidbody 컴포넌트가 없습니다");
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * 800.0f, ForceMode2D.Impulse);
    }
}
