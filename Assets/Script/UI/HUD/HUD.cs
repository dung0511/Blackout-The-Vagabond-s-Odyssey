using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    public Slider healthBar;
    public Slider armorBar;
    public TextMeshProUGUI coins;
    public GameObject skillsUI;
    public GameObject weaponStatus;

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

    public void ShowHUD()
    {
        healthBar.gameObject.SetActive(true);
        armorBar.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);
        skillsUI.gameObject.SetActive(true);
        weaponStatus.gameObject.SetActive(true);
    }
}
