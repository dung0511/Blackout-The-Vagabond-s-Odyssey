using System;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        UIManager.Instance.shieldBarEvent.AddListener(SetShieldBar);
    }

    private void SetShieldBar(float shield, float maxShield)
    {
        healthSlider.value = shield / maxShield;    
    }

    private void OnDestroy()
    {
        UIManager.Instance.healthBarEvent.RemoveListener(SetShieldBar);
    }
}
