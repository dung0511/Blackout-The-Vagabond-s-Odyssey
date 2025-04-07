using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public IUIScreen openedScreen;
    public UnityEvent<float,float> healthBarEvent;
    public UnityEvent<float,float> shieldBarEvent;

    //private Stack<IUIScreen> uiStack = new(); //stack for closing ui

    #region Singleton
    public static UIManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(healthBarEvent == null) healthBarEvent = new();
        if(shieldBarEvent == null) shieldBarEvent = new();
    }
    #endregion

    public void ToggleScreen(IUIScreen screen)
    {
        if (screen == null) return;

        if (openedScreen == screen)
        {
            screen.Close();
            openedScreen = null;
        }
        else
        {
            if (openedScreen != null) openedScreen.Close();
            screen.Open();
            openedScreen = screen;
        }
    }
}
