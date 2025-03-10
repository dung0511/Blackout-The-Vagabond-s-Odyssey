using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; 
    private bool isPaused = false; 

    void Start()
    {
        pauseMenu.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Pause(); 
            }
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}