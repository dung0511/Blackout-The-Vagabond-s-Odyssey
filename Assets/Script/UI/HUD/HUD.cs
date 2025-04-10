using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }
    public Slider healthBar;
    public Slider armorBar;
    public TextMeshProUGUI coins;

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

    public void Show()
    {
        healthBar.gameObject.SetActive(true);
        armorBar.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);
    }
}
