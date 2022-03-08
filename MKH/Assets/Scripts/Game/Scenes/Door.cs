using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    enum State
    {
        Open, Close, Count
    };

    Vector3 originPosition = Vector3.zero;
    Vector3 targetPosition = Vector3.zero;
    State _eState = State.Count;
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
        switch (_eState)
        {
            case State.Open:
                if (Vector3.Distance(transform.position, originPosition) < moveDistance)
                {
                    transform.Translate((targetPosition - originPosition).normalized * Time.deltaTime * 2.0f);
                }
                break;
            case State.Close:
                break;
            default:
                break;
        }
    }

    public void Open()
    {
        _eState = State.Open;
    }
}
