using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //[SerializeField] private GameObject settingsMenu;

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
