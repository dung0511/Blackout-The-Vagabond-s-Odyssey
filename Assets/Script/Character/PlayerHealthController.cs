using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    private Slider healthBar;
    private int health;
    private int maxHealth;
    private bool isHurt;
    private bool isDead;

    //private void Awake()
    //{
        
    //    maxHealth = health;
    //    UpdateHealthBar(health,maxHealth);
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Player>().health;
        isHurt = GetComponent<Player>().isHurt;
        isDead = GetComponent<Player>().isDead;
        maxHealth = health;
        // ton rss
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

    public bool IsDead { get { return isDead; } }
    public bool IsHurt { get { return isHurt; } }
}
