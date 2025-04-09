using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;

    void Start()
    {
        //settingsMenu.SetActive(false); 
        tutorialPanel.SetActive(false);
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
