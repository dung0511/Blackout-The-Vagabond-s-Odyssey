using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmorController : MonoBehaviour
{
    private int currentArmor;
    [SerializeField]
    private int maxArmor;
    private float timeHealArmor = 7f;
    private float lastDamageTime;
    private bool isRegenerating = false;
    private int defaultArmor;
    void Start()
    {

        currentArmor = GetComponent<Player>().armor;
        maxArmor = currentArmor;
        defaultArmor = maxArmor;
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
        UIManager.Instance.shieldBarEvent.Invoke(currentValue, maxValue);
    }

    public void ArmorWhenPlayerDie()
    {
        currentArmor = 0;
        maxArmor = 0;
    }

    public void ShieldSkill()
    {
        currentArmor += 10;
        maxArmor += 10;
        UpdateArmorBar(currentArmor, maxArmor);
    }

    public void EndShieldSkill()
    {
        if (currentArmor >= defaultArmor)
        {
            currentArmor = defaultArmor;
        }

        maxArmor = defaultArmor;
        UpdateArmorBar(currentArmor, maxArmor);
    }

    public int DameTakenDuringShieldSkill()
    {
        return maxArmor + 10 - currentArmor;
    }

    public void IncreaseMaxArmor(int amount)
    {
        maxArmor += amount;
        defaultArmor += amount;
        UpdateArmorBar(currentArmor, maxArmor);
    }

}
