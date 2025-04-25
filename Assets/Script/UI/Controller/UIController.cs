using UnityEngine;

public class UIController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsMenuUI.Instance != null)
            {
                SettingsMenuUI.Instance.Show();
            }
        }
    }
}
