using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public IUIScreen currentScreen;
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
    }
    #endregion

    public void Toggle(IUIScreen screen)
    {
        if (screen == null) return;

        if (currentScreen == screen)
        {
            screen.Close();
            currentScreen = null;
        }
        else
        {
            if (currentScreen != null) currentScreen.Close();
            screen.Open();
            currentScreen = screen;
        }
    }
}
