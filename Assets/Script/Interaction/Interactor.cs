using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;
    private Interactable currentInteractable;

    void Awake()
    {
        var trigger = GetComponent<CircleCollider2D>();
        trigger.radius = range;
        trigger.isTrigger = true;
        trigger.includeLayers = LayerMask.GetMask("Interactable");
    }

    void OnEnable()
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed += Interact;
        InputManager.Instance.playerInput.Ingame.Interact.Enable();
    }

    void OnDisable()
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed -= Interact;
        InputManager.Instance.playerInput.Ingame.Interact.Disable();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable interactable))
        {
            if (currentInteractable != interactable)
            {
                if (currentInteractable != null) currentInteractable.HighLightOff();
                interactable.HighLightOn();
                currentInteractable = interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentInteractable != null)
        {
            currentInteractable.HighLightOff();
            currentInteractable = null;
        }
    }


    private void Interact(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();

            currentInteractable.HighLightOff(); //if teleport away from trigger range
            currentInteractable = null;
        }
        else
        {
            Debug.Log("No interactable in range");
        }
    }
}
