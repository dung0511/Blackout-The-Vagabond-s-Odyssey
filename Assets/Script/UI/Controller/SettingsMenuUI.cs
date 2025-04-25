using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    public static SettingsMenuUI Instance { get; private set; }

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private SoundMixerManager soundMixerManager;

    private bool isPaused = false;
    public bool isOpenSettingMenu = false;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SoundFXVolume";



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        settingsPanel.SetActive(false);
        LoadSavedVolume();

        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
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
        isOpenSettingMenu = !isOpenSettingMenu;
        isPaused = !isPaused;
        settingsPanel?.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        isOpenSettingMenu = false;
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    

    public void QuitGame()
    {
        isOpenSettingMenu = false;
        Time.timeScale = 1f;
        DontDestroyCleaner.ClearDDOL();
        SceneManager.LoadScene("Main Menu");
    }

    private void OnMusicSliderChanged(float value)
    {
        soundMixerManager.setMusicVolume(value);
        PlayerPrefs.SetFloat(MusicKey, value);
    }

    private void OnSFXSliderChanged(float value)
    {
        soundMixerManager.setSoundFXVolume(value);
        PlayerPrefs.SetFloat(SFXKey, value);
    }

    private void LoadSavedVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 0.75f);  // default: 75%
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 0.75f);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        soundMixerManager.setMusicVolume(musicVolume);
        soundMixerManager.setSoundFXVolume(sfxVolume);
    }

}