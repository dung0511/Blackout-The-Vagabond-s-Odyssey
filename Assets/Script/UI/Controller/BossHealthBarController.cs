using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public static BossHealthBarController Instance { get; private set; }

    [SerializeField] private Slider slider;
    private IHealthController target;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameObject.SetActive(false); 
    }

    public void SetTarget(IHealthController newTarget)
    {
        if (target != null)
        {
            target.OnHealthChanged.RemoveListener(UpdateSlider);
        }

        target = newTarget;

        if (target != null)
        {
            slider.maxValue = target.MaxHealth;
            slider.value = target.CurrentHealth;
            target.OnHealthChanged.AddListener(UpdateSlider);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
