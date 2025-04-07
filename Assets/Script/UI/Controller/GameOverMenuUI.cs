using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuUI : MonoBehaviour
{
    [SerializeField] GameObject gameoverMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameoverMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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
