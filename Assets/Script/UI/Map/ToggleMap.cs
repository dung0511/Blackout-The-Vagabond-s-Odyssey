using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleMap : MonoBehaviour
{
    [SerializeField] private GameObject minimap, map;
    [SerializeField] private InputActionReference toggleMapAction;

    private void OnEnable()   
    {
        toggleMapAction.action.Enable();
        toggleMapAction.action.performed += Toggle;
    }

    private void OnDisable()
    {
        toggleMapAction.action.performed -= Toggle;
        toggleMapAction.action.Disable();
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            minimap.SetActive(!minimap.activeSelf);
            map.SetActive(!map.activeSelf);
        }
    }
}
