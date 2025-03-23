using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightFlicker : MonoBehaviour
{
    private Light2D l;
    public float minFalloff = 0.2f;
    public float maxFalloff = 1f;
    public float lerpSpeed = 0.1f;
    public float flickerInterval = 1f; 
    private float targetIntensity;
    private float timer;

    void Start()
    {
        l = GetComponent<Light2D>();
        SetNewTargets();
    }

    void Update()
    {
        if (l)
        {
            timer += Time.deltaTime;
            l.falloffIntensity = Mathf.Lerp(l.falloffIntensity, targetIntensity, lerpSpeed * Time.deltaTime);

            if (timer >= flickerInterval)
            {
                SetNewTargets();
                timer = 0;
            }
        }
    }

    void SetNewTargets()
    {
        targetIntensity = Random.Range(minFalloff, maxFalloff);
    }
}
