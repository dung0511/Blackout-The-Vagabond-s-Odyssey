using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FadeInOut : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public float displayDuration = 2f;

    private List<Graphic> graphics = new List<Graphic>();

    private void Start()
    {
        graphics.AddRange(GetComponentsInChildren<Graphic>(true));
        StartCoroutine(FadeTime());
    }

    IEnumerator FadeTime()
    {
        yield return Fade(0f, 1f); // Fade In
        yield return new WaitForSeconds(displayDuration);
        yield return Fade(1f, 0f); // Fade Out
    }

    IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            foreach (var g in graphics)
            {
                if (g != null)
                {
                    Color c = g.color;
                    g.color = new Color(c.r, c.g, c.b, alpha);
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        foreach (var g in graphics)
        {
            if (g != null)
            {
                Color c = g.color;
                g.color = new Color(c.r, c.g, c.b, end);
            }
        }
    }
}
