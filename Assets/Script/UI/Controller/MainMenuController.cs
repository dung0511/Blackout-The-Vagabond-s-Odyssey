using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    void Start()
    {
        //settingsMenu.SetActive(false); 
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Home"); 
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
