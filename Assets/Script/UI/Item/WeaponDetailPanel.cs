using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WeaponDetailPanel : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI forceText;

    public void UpdateWeaponDetail(WeaponDetailSO detail)
    {
        if (detail == null)
        {
            Debug.LogWarning("WeaponDetailSO is null!");
            return;
        }
        damageText.text =  detail.damageWeapon.ToString();
        cooldownText.text =  detail.attackCooldown.ToString();
        forceText.text =  detail.force.ToString();
    }
}
