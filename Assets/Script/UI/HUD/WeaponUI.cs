using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public GameObject Weapon1;
    public Image weapon1Imange;
    public GameObject Weapon2;
    public Image weapon2Imange;
    public static WeaponUI Instance;

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

    public void SetImageAndActiveWeapon1(Sprite img)
    {
        weapon1Imange.sprite = img;
        weapon1Imange.SetNativeSize();
        Weapon1.transform.localScale = new Vector3(2.5f, 2.5f, 0);
        if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
    }
    public void SetImageAndActiveWeapon2(Sprite img)
    {
        if (!Weapon2.activeInHierarchy)
        {
            Weapon2.SetActive(true);
        }
        weapon2Imange.sprite = img;
        weapon2Imange.SetNativeSize();
        Weapon2.transform.localScale = new Vector3(2.5f, 2.5f, 0);
        Weapon1.transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }
}
