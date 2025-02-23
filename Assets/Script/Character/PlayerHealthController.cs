using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public Slider healthBar;
    public int health;
    public int maxHealth;
    public bool isHurt;
    public bool isDead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Player>().health;
        isHurt = false;
        isDead = false;
        maxHealth = health;
        healthBar = GameObject.Find("HealthBar").gameObject.GetComponent<Slider>();
        UpdateHealthBar(health, maxHealth);
        
    }

    public void takeDame(int dame)
    {
        if (GetComponent<PlayerArmorController>().TakeDamageArmor(dame) <= 0)
        {
            health -= dame;
            UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {
                isDead = true;
                health = 0;
                //animator.SetBool("isDead",true);
            }
            else isHurt = true;
            StartCoroutine(ResetHurt());
        }
        isHurt = true;
        StartCoroutine(ResetHurt());
    }
    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void UpdateHealthBar(int currentValue, int maxValue)
    {
        healthBar.value = (float)currentValue / (float)maxValue;
    }
}
