using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public static SkillCooldownUI Instance;

    // UI Elements
    public Image skillIcon_E;
    public Image cooldownMask_E;
    public TextMeshProUGUI cooldownText_E;

    public Image skillIcon_Q;
    public Image cooldownMask_Q;
    public TextMeshProUGUI cooldownText_Q;

    // Cooldown Tracking
    private float cooldownTime_E, currentTime_E;
    private float cooldownTime_Q, currentTime_Q;
    private bool isCoolingDown_E, isCoolingDown_Q;

    //for Wizard only
    private bool isReactivating_E = false;
    public Image glowEffect_E;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSkillIcons();
        }
        else
        {
            Destroy(gameObject);
        }

        cooldownText_E.gameObject.SetActive(false);
        cooldownText_Q.gameObject.SetActive(false);
        glowEffect_E.gameObject.SetActive(false);
    }

    public void InitializeSkillIcons()
    {
        //E
        skillIcon_E.sprite = GameManager.Instance.playerCharacter.characterSkillIcon1;
        skillIcon_E.enabled = true;
        cooldownMask_E.sprite = GameManager.Instance.playerCharacter.characterSkillIcon1;

        //Q
        skillIcon_Q.sprite = GameManager.Instance.playerCharacter.characterSkillIcon2;
        skillIcon_Q.enabled = true;
        cooldownMask_Q.sprite = GameManager.Instance.playerCharacter.characterSkillIcon2;
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

    //lan1
    public void StartWizardESkill()
    {
        if (isCoolingDown_E) return;

        isReactivating_E = true;
        glowEffect_E.enabled = true;
    }

    //lan 2
    public void ActivateWizardESkill(float cooldownTime)
    {
        if (!isReactivating_E) return;

        cooldownTime_E = cooldownTime;
        currentTime_E = cooldownTime;
        isCoolingDown_E = true;
        isReactivating_E = false;

        cooldownMask_E.fillAmount = 1f;
        cooldownText_E.text = Mathf.Ceil(cooldownTime).ToString();
        cooldownText_E.gameObject.SetActive(true);
        glowEffect_E.enabled = false;
    }

    
}