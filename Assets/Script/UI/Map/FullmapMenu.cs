using UnityEngine;
using UnityEngine.InputSystem;

public class FullmapMenu : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 100f;
    [SerializeField] private Camera mapCamera;
    [SerializeField] private InputActionReference zoomAction;
    [SerializeField] private InputActionReference panAction;

    private void OnEnable()
    {
        zoomAction.action.Enable();
        panAction.action.Enable();
        zoomAction.action.performed += Zoom;
        panAction.action.performed += Pan;
    }

    private void OnDisable()
    {
        zoomAction.action.performed -= Zoom;
        panAction.action.performed -= Pan;
        zoomAction.action.Disable();
        panAction.action.Disable();
    }

    private void Zoom(InputAction.CallbackContext context)
    {
        float zoom = mapCamera.orthographicSize;
        zoom -= context.ReadValue<float>() * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        mapCamera.orthographicSize = zoom;
    }

     private void Pan(InputAction.CallbackContext context)
    {
        if (Mouse.current.leftButton.isPressed) // fixed left click to pan
        {
            Vector2 mouseDelta = context.ReadValue<Vector2>(); // mouse pos change each frames
            Vector3 move = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * panSpeed; 
            mapCamera.transform.position += move;
        }
    }
}