using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "Interaction/InputData")]
public class InteractionInputData : ScriptableObject
{
    private bool interactionInputBegin;
    private bool interactionInputEnd;

    public bool InteractionInputBegin
    {
        get => interactionInputBegin;
        set => interactionInputBegin = value;
    }
    public bool InteractionInputEnd
    {
        get => interactionInputEnd;
        set => interactionInputEnd = value;
    }

    public void Reset()
    {
        interactionInputBegin = false;
        interactionInputEnd = false;
    }
}
