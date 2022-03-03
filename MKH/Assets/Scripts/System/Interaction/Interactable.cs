using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    enum InteractionType { Tap, Hold, Count }

    float HoldDuration { get; }
    InteractionType Interaction { get; }
    bool IsMultipleUse { get; }
    bool IsActivated { get; }

    void OnInteract();
}

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    public bool _isActivated;
    public bool IsActivated => _isActivated;

    [Space]
    public IInteractable.InteractionType _interactionType;
    public float holdDuration;
    public float HoldDuration => holdDuration;
    public IInteractable.InteractionType Interaction => _interactionType;

    [Space]
    public bool _isMultipleUse;
    public int _maxUseCount = 1;
    public int _useCount;

    public bool IsMultipleUse => _isMultipleUse;
    public int MaxUseCount => _maxUseCount;
    public int UseCount => _useCount;

    public void AddUseCount(in int addValue)
    {
        _useCount += addValue;
    }

    public bool IsUsedAll()
    {
        return UseCount >= MaxUseCount;
    }


    public void Toggle()
    {
        _isActivated = !_isActivated;
    }

    public void Active()
    {
        _isActivated = true;
    }

    public void DeActive()
    {
        _isActivated = false;
    }

    public virtual void OnInteract()
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
}