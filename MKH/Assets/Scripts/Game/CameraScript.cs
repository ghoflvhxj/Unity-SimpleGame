using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float distance = 10.0f;
    private float lerpFactor = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPosition = new Vector3(0.0f, transform.position.y, transform.position.z);
        Vector3 playerPosition = new Vector3(0.0f, player.transform.position.y, player.transform.position.z);
        Vector3 cameraYZposition = Vector3.Lerp(cameraPosition, playerPosition, Vector3.Distance(cameraPosition, playerPosition) / lerpFactor);
        Vector3 cameraZPosition = new Vector3(distance, 0.0f, 0.0f);
        transform.position = cameraYZposition + cameraZPosition;
    }
}
