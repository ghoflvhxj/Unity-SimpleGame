using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public enum DamagedAction
    {
        None, KnockBack, KnockUp, Count
    };

    [SerializeField] DamagedAction _eDamagedAction;

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
        Character character = other.gameObject.GetComponent<Character>();
        if (null == character)
        {
            return;
        }

        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if(null == rigidbody)
        {
            return;
        }

        character.Hit(10);

        switch(_eDamagedAction)
        {
            case DamagedAction.KnockBack:
                Vector3 direction = (new Vector3(0f, 0f, other.gameObject.transform.position.z - transform.position.z) + Vector3.up). normalized;
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(direction * 10.0f, ForceMode.VelocityChange);
                break;
            case DamagedAction.KnockUp:
                break;
            default:
                break;
        }
    }
}
