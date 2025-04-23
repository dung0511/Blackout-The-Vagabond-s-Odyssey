using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    private Slider healthSlider;
    private TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GetComponent<Slider>();
        UIManager.Instance.shieldBarEvent.AddListener(SetShieldBar);
    }

    private void SetShieldBar(float shield, float maxShield)
    {
        healthSlider.value = shield / maxShield;
        textMeshPro.text = shield.ToString() + " / " + maxShield.ToString();
    }

    private void OnDestroy()
    {
        UIManager.Instance.healthBarEvent.RemoveListener(SetShieldBar);
    }
}
