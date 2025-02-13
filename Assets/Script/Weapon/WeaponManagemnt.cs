using UnityEngine;

public class WeaponManagemnt : MonoBehaviour
{
    int weaponTotal = 1;
    public int currentWeaponIndex;
    public GameObject[] weapons;
    public GameObject weaponContainer;
    public GameObject currentWeapon;
    void Start()
    {
        weaponTotal = weaponContainer.transform.childCount;
        weapons = new GameObject[weaponTotal];
        for (int i = 0; i < weaponTotal; i++)
        {
            weapons[i] = weaponContainer.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        //if (GameManager.currentWeaponIndex > 0)
        //{
        //    weapons[GameManager.currentWeaponIndex].SetActive(true);
        //    currentWeapon = weapons[GameManager.currentWeaponIndex];
        //}
        //else
        //{
            weapons[0].SetActive(true);
            currentWeapon = weapons[0];
        //}
    }
}
