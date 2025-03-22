using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D l;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float minRadius, maxRadius;
    public float intensityChangeSpeed = 0.1f;
    public float radiusChangeSpeed = 0.5f;
    public float flickerInterval = 1f; 
    public bool customRadius = false;

    private float targetIntensity;
    private float targetRadius;
    private float timer;

    void Start()
    {
        l = GetComponent<Light2D>();
        if (l && !customRadius)
        {
            minRadius = (l.pointLightInnerRadius + l.pointLightOuterRadius) / 2f;
            maxRadius = l.pointLightOuterRadius;
        }
        SetNewTargets();
    }

    void Update()
    {
        if (l)
        {
            timer += Time.deltaTime;
            l.intensity = Mathf.Lerp(l.intensity, targetIntensity, intensityChangeSpeed);
            l.pointLightOuterRadius = Mathf.Lerp(l.pointLightOuterRadius, targetRadius, radiusChangeSpeed);

            if (timer >= flickerInterval)
            {
                SetNewTargets();
                timer = 0;
            }
        }
    }

    void SetNewTargets()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        targetRadius = Random.Range(minRadius, maxRadius);
    }
}
