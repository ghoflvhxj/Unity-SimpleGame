using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Vector3 originPosition = Vector3.zero;
    Vector3 targetPosition = Vector3.zero;
    float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        BoxCollider collider = GetComponent<BoxCollider>();

        moveDistance = transform.lossyScale.y;
        targetPosition = transform.position - new Vector3(0.0f, moveDistance, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(true == Input.GetKey(KeyCode.C))
        {
            Open();
        }

    }

    public void Open()
    {
        if (Vector3.Distance(transform.position, originPosition) < moveDistance)
        {
            transform.Translate((targetPosition - originPosition).normalized * Time.deltaTime * 2.0f);
        }
    }
}
