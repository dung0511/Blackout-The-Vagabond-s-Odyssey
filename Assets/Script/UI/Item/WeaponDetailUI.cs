using UnityEngine;

public class WeaponDetailUI : MonoBehaviour
{
    public static WeaponDetailUI weaponDetailUI { get; private set; }
    public WeaponDetailPanel weaponPanel;

    private void Awake()
    {
        if (weaponDetailUI == null)
        {
            weaponDetailUI = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdatePanel(WeaponDetailSO weaponDetailSO)
    {
        weaponPanel.UpdateWeaponDetail(weaponDetailSO);
    }
    public void showPanel()
    {
        weaponPanel.gameObject.SetActive(true);
        //weaponPanel.enabled = true;
    }
    public void hidePanel()
    {
        weaponPanel.gameObject.SetActive(false);
        //weaponPanel.enabled = false;
    }
}
