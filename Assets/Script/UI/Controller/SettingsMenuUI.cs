using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    public static SettingsMenuUI Instance { get; private set; }

    [SerializeField] private GameObject settingsPanel;
    //[SerializeField] private Slider musicSlider;
    //[SerializeField] private Slider vfxSlider;

    private bool isPaused = false;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnSceneLoaded;
        //LoadSettings();
    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    musicSlider = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
    //    vfxSlider = GameObject.Find("VFXSlider")?.GetComponent<Slider>();
    //    LoadSettings();
    //}

    //private void LoadSettings()
    //{
    //    float musicVol = PlayerPrefs.GetFloat("musicVolume", 1f);
    //    float vfxVol = PlayerPrefs.GetFloat("vfxVolume", 1f);
    //    Debug.Log($"Loaded Music Volume: {musicVol}, VFX Volume: {vfxVol}");
    //    if (musicSlider != null)
    //    {
    //        musicSlider.value = musicVol;
    //        AudioListener.volume = musicVol;
    //    }

    //    if (vfxSlider != null)
    //    {
    //        vfxSlider.value = vfxVol;
    //    }
    //}

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
        settingsPanel?.SetActive(isPaused);
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
        DataManager.Save();
        Time.timeScale = 1f;
        DontDestroyCleaner.ClearDDOL();
        SceneManager.LoadScene("Main Menu");
    }

    //public void OnMusicVolumeChanged(float value)
    //{
    //    PlayerPrefs.SetFloat("musicVolume", value);
    //    PlayerPrefs.Save();
    //    AudioListener.volume = value;
    //}

    //public void OnVFXVolumeChanged(float value)
    //{
    //    PlayerPrefs.SetFloat("vfxVolume", value);
    //    PlayerPrefs.Save();
    //}
}