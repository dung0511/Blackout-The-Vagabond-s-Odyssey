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
            Destroy(GameObject.Find("HomeBGM"));
            SceneManager.LoadScene("Home"); 
        }
    }

    private void OnDestroy()
    {
        if (director != null)
            director.stopped -= OnCutsceneEnd;
    }
}
