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
    //public GameObject weaponStatus;
    public GameObject Image;
    public GameObject Bag;
    public GameObject Setting;

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
        Image.SetActive(true);
        healthBar.gameObject.SetActive(true);
        armorBar.gameObject.SetActive(true);
        coins.gameObject.SetActive(true);
        skillsUI.gameObject.SetActive(true);
        Bag.SetActive(true);
        Setting.SetActive(true);
        //weaponStatus.gameObject.SetActive(true);
    }
}
