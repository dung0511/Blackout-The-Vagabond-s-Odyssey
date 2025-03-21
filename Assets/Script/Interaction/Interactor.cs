using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;
    [SerializeField] private LayerMask interactableLayer;
    private Interactable previousInteract;

    void OnEnable()
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed += Interact;
        InputManager.Instance.playerInput.Ingame.Interact.Enable();
    }

    void OnDisable()
    {
        InputManager.Instance.playerInput.Ingame.Interact.performed += Interact;
        InputManager.Instance.playerInput.Ingame.Interact.Disable();
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, interactableLayer);

        if (hit == null) //no interactable in range
        {
            if (previousInteract != null) //turn off previous object when out of range
            {
                previousInteract.HighLightOff();
                previousInteract = null;
            }
        }
        else
        {
            if (hit.TryGetComponent<Interactable>(out Interactable newTarget))
            {
                if (previousInteract != newTarget)
                {
                    newTarget.HighLightOn(); //hightlight new found tagert
                    if (previousInteract != null) previousInteract.HighLightOff(); //turn off previous target
                    previousInteract = newTarget;
                }
            }
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (previousInteract != null)
        {
            previousInteract.Interact();
        }
        else
        {
            Debug.Log("No interactable in range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
