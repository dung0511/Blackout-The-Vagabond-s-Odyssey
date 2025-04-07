using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldownUI : MonoBehaviour
{
    public Image cooldownMask;
    public TextMeshProUGUI cooldownText;

    private float cooldownTime;
    private float currentTime;
    private bool isCoolingDown;

    private void Awake()
    {
        cooldownText.gameObject.SetActive(false); 
    }

    private void Update()
    {
        if (isCoolingDown)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                cooldownMask.fillAmount = 0;
                cooldownText.text = "";
                cooldownText.gameObject.SetActive(false);
                isCoolingDown = false;
            }
            else
            {
                cooldownMask.fillAmount = currentTime / cooldownTime;
                cooldownText.text = Mathf.Ceil(currentTime).ToString();
            }
        }
    }

    public void TriggerCooldown(float time)
    {
        cooldownTime = time;
        currentTime = time;
        isCoolingDown = true;

        cooldownMask.fillAmount = 1f;
        cooldownText.text = Mathf.Ceil(time).ToString();
        cooldownText.gameObject.SetActive(true);
    }
}
