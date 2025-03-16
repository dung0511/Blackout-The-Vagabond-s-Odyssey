using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private InputActionReference interact;
    private IInteractable interactTarget;

    void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += Interact;
    }

    void OnDisable()
    {
        interact.action.performed -= Interact;
        interact.action.Disable();
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, interactableLayer);
        if(hit == null)
        {
            var previousTarget = interactTarget;
            interactTarget = null;
            if (previousTarget != null)
            {
                previousTarget.HighLightOff();
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



