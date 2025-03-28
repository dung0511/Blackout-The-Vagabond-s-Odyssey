using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))] //animation event for lerp target
public class AnimationBasedLight : MonoBehaviour
{
    [SerializeField, InspectorName("Light")] private Light2D l;
    [SerializeField] private float lerpSpeed = 1f;
    int target = 0;

    void Awake()
    {
        if(!l) l = GetComponentInChildren<Light2D>();
    }

    private void SetLerpTarget(int target)
    {
        this.target = target;
    }

    void Update()
    {
        l.intensity = Mathf.Lerp(l.intensity, target, lerpSpeed*Time.deltaTime);
    }
}
