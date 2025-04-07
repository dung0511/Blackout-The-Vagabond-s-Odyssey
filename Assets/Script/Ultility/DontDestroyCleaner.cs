using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCleaner : MonoBehaviour
{
    public static void DestroyAllDontDestroyOnLoadObjects()
    {
        GameObject temp = new GameObject("Temp_DDOL");
        DontDestroyOnLoad(temp);
        Scene ddolScene = temp.scene;
        DestroyImmediate(temp);

        GameObject[] rootObjects = ddolScene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            Destroy(obj);
        }
    }
}
