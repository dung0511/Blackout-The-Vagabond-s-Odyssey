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
        PlayerPrefs.SetInt("HasWatchedCutscene", 0);
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("HasWatchedCutscene", 0) == 0)
        {
            
            SceneManager.LoadScene("Cutscene_BeforeBlackout");
        }
        else
        {
            Destroy(GameObject.Find("HomeBGM"));
            SceneManager.LoadScene("Home");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }

}
