using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class NotificationController : MonoBehaviour
{
    public static NotificationController Instance { get; private set; }

    public TextMeshProUGUI notiText;
    public float displayDuration = 1.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowNotification(string message)
    {
        notiText.text = message;
        //notiText.gameObject.SetActive(true);
        notiText.enabled = true;
        StartCoroutine(HideAfterDelay(displayDuration));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        notiText.enabled = false;
        //notiText.gameObject.SetActive(false);
    }
}
