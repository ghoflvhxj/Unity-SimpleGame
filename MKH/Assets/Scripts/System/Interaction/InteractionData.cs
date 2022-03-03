using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionData", menuName = "Interaction/InteractionData")]
public class InteractionData : ScriptableObject
{
    private Interactable interactable;

    public Interactable Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    public void Interact()
    {
        interactable.OnInteract();

        // 상호작용 후
        if (false == Interactable.IsMultipleUse)
        {
            Interactable.DeActive();
        }
        else
        {
            if (Interactable.IsUsedAll())
            {
                Interactable.DeActive();
            }

            Interactable.AddUseCount(1);
        }

        Reset();
    }

    public bool IsSameInteractable(Interactable newInteractable)
    {
        return (interactable == newInteractable);
    }

    public void Reset()
    {
        interactable = null;
    }

    public bool IsEmpty()
    {
        return (null == interactable);
    }
}
