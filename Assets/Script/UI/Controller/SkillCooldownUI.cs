using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public static SkillCooldownUI Instance;

    public Image cooldownMask_E;
    public TextMeshProUGUI cooldownText_E;
    private float cooldownTime_E;
    private float currentTime_E;
    private bool isCoolingDown_E;

    public Image cooldownMask_Q;
    public TextMeshProUGUI cooldownText_Q;
    private float cooldownTime_Q;
    private float currentTime_Q;
    private bool isCoolingDown_Q;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        cooldownText_E.gameObject.SetActive(false);
        cooldownText_Q.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isCoolingDown_E)
        {
            currentTime_E -= Time.deltaTime;
            if (currentTime_E <= 0)
            {
                cooldownMask_E.fillAmount = 0;
                cooldownText_E.text = "";
                cooldownText_E.gameObject.SetActive(false);
                isCoolingDown_E = false;
            }
            else
            {
                cooldownMask_E.fillAmount = currentTime_E / cooldownTime_E;
                cooldownText_E.text = Mathf.Ceil(currentTime_E).ToString();
            }
        }

        if (isCoolingDown_Q)
        {
            currentTime_Q -= Time.deltaTime;
            if (currentTime_Q <= 0)
            {
                cooldownMask_Q.fillAmount = 0;
                cooldownText_Q.text = "";
                cooldownText_Q.gameObject.SetActive(false);
                isCoolingDown_Q = false;
            }
            else
            {
                cooldownMask_Q.fillAmount = currentTime_Q / cooldownTime_Q;
                cooldownText_Q.text = Mathf.Ceil(currentTime_Q).ToString();
            }
        }
    }

    public void TriggerCooldown_E(float time)
    {
        cooldownTime_E = time;
        currentTime_E = time;
        isCoolingDown_E = true;

        cooldownMask_E.fillAmount = 1f;
        cooldownText_E.text = Mathf.Ceil(time).ToString();
        cooldownText_E.gameObject.SetActive(true);
    }

    public void TriggerCooldown_Q(float time)
    {
        cooldownTime_Q = time;
        currentTime_Q = time;
        isCoolingDown_Q = true;

        cooldownMask_Q.fillAmount = 1f;
        cooldownText_Q.text = Mathf.Ceil(time).ToString();
        cooldownText_Q.gameObject.SetActive(true);
    }
}