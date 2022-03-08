using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    enum State
    {
        Ready, Work, Back, Count
    }

    State _eState = State.Ready;

    [SerializeField] private GameObject playerDetecter = null;
    [SerializeField] private GameObject bounder = null;

    [Header("RaidTrap")]
    [SerializeField] private float minLerp = 0.001f;
    [SerializeField] private float lerpFactor = 100f;

    private Vector3 originPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        targetPosition += bounder.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (State.Ready == _eState)
        {
            Detecter detecter = playerDetecter.GetComponent<Detecter>();

            if (true == detecter.Detected)
            {
                _eState = State.Work;
            }
        }


        //---------------------------------------
        if (State.Work == _eState)
        {
            if((transform.position.y - (transform.localScale.y / 2f)) <= targetPosition.y)
            {
                _eState = State.Back;
            }
            else
            {
                float lerp = Vector3.Distance(originPosition, transform.position) / lerpFactor;
                if (lerp < minLerp)
                {
                    lerp = minLerp;
                }

                transform.position = Vector3.Lerp(transform.position, targetPosition, lerp);
            }
        }

        if(State.Back == _eState)
        {
            if(transform.position.y < originPosition.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            else
            {
                transform.position = originPosition;
                _eState = State.Ready;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(false == collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Character playerCharacter = collision.gameObject.GetComponent<Character>();
        playerCharacter.AddHealth(-playerCharacter.Health);
    }
}
