using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InteractionInputData interactionInputData;
    public KeyCode interactionKey;

    // Start is called before the first frame update
    void Start()
    {
        interactionInputData.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractionInputData();
    }

    private void GetInteractionInputData()
    {
        interactionInputData.InteractionInputBegin = Input.GetKeyDown(KeyCode.E);
        Debug.Log("E CLICKED" + interactionInputData.InteractionInputBegin);
        interactionInputData.InteractionInputEnd = Input.GetKeyUp(KeyCode.E);
        Debug.Log("E RELEASED" + interactionInputData.InteractionInputEnd);
    }
}
