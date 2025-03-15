using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private InputAction interactAction;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable interactTarget;

    void Start()
    {
        interactAction = playerInput.actions["Interact"];
        interactAction.performed += Interact;
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, interactableLayer);
        if(hit == null)
        {
            if(interactTarget != null)
            {
                interactTarget.HighLightOff();
                interactTarget = null;
            }
        } else
        {
            if(hit.TryGetComponent<IInteractable>(out interactTarget))
            {
                interactTarget.HighLightOn();
            }
        }
        
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(interactTarget != null)
        {
            Debug.Log("Interactable in range");
            interactTarget.Interact();
        } else 
        {
            Debug.Log("No interactable in range");
        }
    }
}



