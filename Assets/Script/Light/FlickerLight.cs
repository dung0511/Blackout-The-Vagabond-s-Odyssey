using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlickerLight : MonoBehaviour
{
    private Light2D l;
    public float minFalloff = 0.2f;
    public float maxFalloff = 1f;
    public float lerpSpeed = 0.1f;
    public float flickerInterval = 1f; 
    private float targetFalloff;
    private float targetInnerRadius;
    private float timer;
    private float defaultInnerRadius;
    void Start()
    {
        l = GetComponent<Light2D>();
        defaultInnerRadius = l.pointLightInnerRadius;
        SetNewTargets();
    }

    void Update()
    {
        if (l)
        {
            timer += Time.deltaTime;
            l.falloffIntensity = Mathf.Lerp(l.falloffIntensity, targetFalloff, lerpSpeed * Time.deltaTime);
            l.pointLightInnerRadius = Mathf.Lerp(l.pointLightInnerRadius, targetInnerRadius, lerpSpeed * Time.deltaTime);
            if (timer >= flickerInterval)
            {
                SetNewTargets();
                timer = 0;
            }
        }
    }

    void SetNewTargets()
    {
        targetFalloff = Random.Range(minFalloff, maxFalloff);
        targetInnerRadius = defaultInnerRadius - targetFalloff;
    }
}
