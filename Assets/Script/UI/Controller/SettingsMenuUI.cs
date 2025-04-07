using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    private bool isPaused = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu()
    {
        isPaused = !isPaused;

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(isPaused);
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene("MainMenu");
    }
}