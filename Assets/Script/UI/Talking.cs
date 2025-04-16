using System.Collections;
using TMPro;
using UnityEngine;

public class Talking : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float delay;
    public float delayEndTalk;
    public static Talking INSTANCE;
    private void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    public void Talk(string text)
    {
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        textMesh.text = "";
        foreach (char c in text)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(delay);
        }
        StartCoroutine(EndTalk());
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(delayEndTalk);
        textMesh.text = "";
    }


}
