using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private InteractionInputData _interactionInputData = null;
    [SerializeField] private InteractionData _interactionData = null;

    [Header("UI")]
    [SerializeField] private InteractionUiPannel _interactionUIPannel;

    [Header("RaySettings")]
    [SerializeField] private GameObject _rayCaster;
    [SerializeField] private float _rayRadius = 1.0f;
    [SerializeField] private float _rayDistance = float.MaxValue;
    [SerializeField] private LayerMask _interactableLayer;

    private bool _isInteracting;
    private float _holdTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInteractInput();
        CheckInteractable();
    }

    void CheckInteractInput()
    {
        if(true == _interactionData.IsEmpty())
        {
            return;
        }

        if(true == _interactionInputData.InteractionInputBegin)
        {
            _isInteracting = true;
            _holdTime = 0f;
        }

        if(true == _interactionInputData.InteractionInputEnd)
        {
            _isInteracting = false;
            _holdTime = 0f;
        }

        //-------------------------------------------------------------------
        if(true == _isInteracting)
        {
            if (false == _interactionData.Interactable.IsActivated)
            {
                _interactionUIPannel.SetText("상호작용 불가능");
                return;
            }

            _interactionUIPannel.SetText("상호작용 중...");
            switch (_interactionData.Interactable.Interaction)
            {
                case IInteractable.InteractionType.Hold:
                    HoldInteract();
                    break;
                default:
                    Interact();
                    break;
            }
        }
        else
        {
            if (false == _interactionData.Interactable.IsActivated)
            {
                _interactionUIPannel.SetText("상호작용 불가능");
                return;
            }

            _interactionUIPannel.SetText("E를 눌러 상호작용 하기");
        }
    }

    void CheckInteractable()
    {
        if(null == _rayCaster)
        {
            _rayCaster = gameObject;
        }

        Ray ray = new Ray(_rayCaster.transform.position, _rayCaster.transform.forward);
        RaycastHit rayHitInfo;

        bool isRayHit = Physics.SphereCast(ray, _rayRadius, out rayHitInfo, _rayDistance, _interactableLayer);
        Debug.DrawRay(ray.origin, ray.direction, (isRayHit == true) ? Color.red : Color.green);
        if (false == isRayHit)
        {
            return;
        }

        Interactable interactable = rayHitInfo.collider.gameObject.GetComponent<Interactable>();
        if(null == interactable)
        {
            _interactionData.Reset();
            return;
        }

        _interactionData.Interactable = interactable;
    }

    void Interact()
    {
        _interactionData.Interact();
        _isInteracting = false;

        _interactionUIPannel.Reset();
    }

    void HoldInteract()
    {
        _holdTime += Time.deltaTime;

        if(_holdTime >= _interactionData.Interactable.HoldDuration)
        {
            Interact();
        }
    }
}
