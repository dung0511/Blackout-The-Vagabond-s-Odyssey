using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        UIManager.Instance.healthBarEvent.AddListener(SetHealthBar);
    }

    private void SetHealthBar(float health, float maxHealth)
    {
        healthSlider.value = health / maxHealth;    
    }

    private void OnDestroy()
    {
        UIManager.Instance.healthBarEvent.RemoveListener(SetHealthBar);
    }
}
