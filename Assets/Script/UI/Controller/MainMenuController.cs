using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    private static bool isGameStarted = false;

    void Start()
    {
        if(!isGameStarted)
        {
            isGameStarted = true;
            DataManager.Load();
        }
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
        DataManager.Save();
        Application.Quit();
    }
    
}
