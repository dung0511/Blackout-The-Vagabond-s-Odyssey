using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenebeforeblackout : MonoBehaviour
{
   public void changeScene()
    {
        PlayerPrefs.SetInt("HasWatchedCutscene", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("CutsceneNew");
    }
}
