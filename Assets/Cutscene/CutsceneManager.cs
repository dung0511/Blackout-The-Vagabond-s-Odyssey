using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector pd)
    {
       
        if (pd == director)
        {
            PlayerPrefs.SetInt("HasWatchedCutscene", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Home"); 
        }
    }

    private void OnDestroy()
    {
        if (director != null)
            director.stopped -= OnCutsceneEnd;
    }
}
