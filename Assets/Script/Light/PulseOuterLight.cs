using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class PulseOuterLightPerlin : MonoBehaviour
{
    public float minOuter = 1f;
    public float maxOuter = 2f;
    [Tooltip("How fast the Perlin noise pattern evolves over time.")]
    public float noiseSpeed = 1f; 
    [Tooltip("How smoothly the radius transitions towards the target noise value. Lower is smoother.")]
    public float smoothSpeed = 5f;

    private Light2D light2D;
    private float noiseOffset;
    private float targetOuterRadius; // Stores the noise target for smoothing

    void Awake()
    {
        light2D = GetComponent<Light2D>();
        noiseOffset = Random.Range(0f, 1000f);
        // Initialize
        targetOuterRadius = light2D.pointLightOuterRadius;
    }

    void Update()
    {
        // 1. Calculate the Perlin noise coordinates
        // Using time multiplied by speed, plus the unique offset ensures the noise
        // evolves over time and is unique per instance.
        float noiseCoordX = Time.time * noiseSpeed + noiseOffset;
        // Using a slightly different calculation or offset for the second dimension
        // can sometimes produce better evolving 1D noise patterns from the 2D function.
        float noiseCoordY = Time.time * noiseSpeed * 0.5f + noiseOffset + 500f; // Example variation

        // 2. Get the Perlin noise value
        // Mathf.PerlinNoise returns a smoothly varying pseudo-random value between 0.0 and 1.0
        float perlinValue = Mathf.PerlinNoise(noiseCoordX, noiseCoordY);

        // 3. Map the 0-1 noise value directly to the minOuter-maxOuter range
        // This calculated value becomes the target we want to move towards.
        targetOuterRadius = Mathf.Lerp(minOuter, maxOuter, perlinValue);

        // 4. Smoothly interpolate towards the target radius (Optional but recommended)
        // Instead of instantly setting the radius, smoothly move the current radius
        // towards the target value derived from the noise. This prevents jitter.
        light2D.pointLightOuterRadius = Mathf.Lerp(
            light2D.pointLightOuterRadius, // Current value
            targetOuterRadius,             // Target value from noise
            Time.deltaTime * smoothSpeed   // Smoothing factor (adjust smoothSpeed)
        );

        // --- Alternative: Direct Application (No Smoothing) ---
        // If you want instant changes based on the noise (can be jittery):
        // light2D.pointLightOuterRadius = targetOuterRadius;
        // ------------------------------------------------------
    }
}