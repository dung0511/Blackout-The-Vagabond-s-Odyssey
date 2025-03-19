using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerInput; //c# generated class input system
    
    public static InputManager Instance {get; private set;}
    private void Awake()
    {
        Debug.Log("InputManager Awake called"); // Check if this runs
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerInput = new PlayerControls();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}