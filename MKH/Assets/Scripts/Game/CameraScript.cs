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
        Vector3 cameraPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
        Vector3 cameraXYposition = Vector3.Lerp(cameraPosition, playerPosition, Vector3.Distance(cameraPosition, playerPosition) / lerpFactor);
        Vector3 cameraZPosition = new Vector3(0f, 0f, distance);
        transform.position = cameraXYposition + cameraZPosition;
    }
}
