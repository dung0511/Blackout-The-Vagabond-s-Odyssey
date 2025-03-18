using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerInput; //c# generated class input system
    
    public static InputManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerInput = new PlayerControls();
    }

}