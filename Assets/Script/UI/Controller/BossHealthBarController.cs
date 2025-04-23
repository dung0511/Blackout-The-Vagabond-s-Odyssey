using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public static BossHealthBarController Instance { get; private set; }

    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    public void UpdateSlider(float health, float maxhealth)
    {
        slider.value = health / maxhealth;
    }

    public void Show()
    {
        slider.gameObject.SetActive(true);
    }
}
