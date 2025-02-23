using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmorController : MonoBehaviour
{
    public Slider armorBar;
    public int currentArmor;
    public int maxArmor; 
    private float timeHealArmor = 5f;
    private float lastDamageTime;
    private bool isRegenerating = false;

    void Start()
    {
        currentArmor = GetComponent<Player>().armor;
        armorBar = GameObject.Find("ShieldBar").gameObject.GetComponent<Slider>();       
        Debug.Log("sjigbdhjskgb: "+currentArmor);
        maxArmor = currentArmor;
        UpdateArmorBar(currentArmor, maxArmor);
    }

    void Update()
    {
        if (!isRegenerating && (Time.time - lastDamageTime >= timeHealArmor) && currentArmor < maxArmor)
        {
            StartCoroutine(RegenArmor());
        }
    }

    public int TakeDamageArmor(int damage)
    {
        int absorbedDamage = Mathf.Min(damage, currentArmor); 
        currentArmor -= absorbedDamage;

        if (currentArmor <= 0)
        {
            currentArmor = 0;
        }

        UpdateArmorBar(currentArmor, maxArmor);
        lastDamageTime = Time.time;

        if (isRegenerating)
        {
            StopCoroutine(RegenArmor());
            isRegenerating = false;
        }

        return absorbedDamage; 
    }


    private IEnumerator RegenArmor()
    {
        isRegenerating = true;

        while (currentArmor < maxArmor)
        {
            currentArmor++;
            UpdateArmorBar(currentArmor, maxArmor);
            yield return new WaitForSeconds(1f);

            if (Time.time - lastDamageTime < timeHealArmor)
            {
                isRegenerating = false;
                yield break;
            }
        }
        isRegenerating = false;
    }

    public void AddArmor()
    {
        maxArmor++;
        currentArmor++;
        UpdateArmorBar(currentArmor, maxArmor);
    }

    public void UpdateArmorBar(int currentValue, int maxValue)
    {
        armorBar.value = (float)currentValue / (float)maxValue;
    }
}
