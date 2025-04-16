using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndGameManager : MonoBehaviour
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
            
            SceneManager.LoadScene("Home");
        }
    }

    private void OnDestroy()
    {
        if (director != null)
            director.stopped -= OnCutsceneEnd;
    }
}
