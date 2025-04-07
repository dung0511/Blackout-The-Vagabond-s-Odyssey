using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] GameObject gameoverMenu;

    void Start()
    {
        gameoverMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void Show()
    {
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
