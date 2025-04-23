using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    private TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GetComponent<Slider>();
        UIManager.Instance.healthBarEvent.AddListener(SetHealthBar);
    }

    private void SetHealthBar(float health, float maxHealth)
    {       
        healthSlider.value = health / maxHealth;
        textMeshPro.text = health.ToString() + " / " + maxHealth.ToString();
    }

    private void OnDestroy()
    {
        UIManager.Instance.healthBarEvent.RemoveListener(SetHealthBar);
    }
}
