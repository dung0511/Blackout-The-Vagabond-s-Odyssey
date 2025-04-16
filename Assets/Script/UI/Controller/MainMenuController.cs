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

        
        if (PlayerPrefs.GetInt("HasWatchedCutscene", 0) == 0)
        {
            
            SceneManager.LoadScene("CutsceneNew");
        }
        else
        {
            
            SceneManager.LoadScene("Home");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
