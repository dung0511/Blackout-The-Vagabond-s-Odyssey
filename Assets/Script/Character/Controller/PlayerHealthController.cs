using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Damageable
{
    public int maxHealth;
    private bool isHurt;
    private bool isDead;

    //private void Awake()s
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
        UpdateHealthBar(maxHealth, maxHealth);
    }

    public override void takeDame(int dame)
    {
        if (GetComponent<PlayerArmorController>().TakeDamageArmor(dame) <= 0)
        {
            health -= dame;
            UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {
                isDead = true;
                health = 0;
                ShowGameOverMenu();
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

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        UIManager.Instance.healthBarEvent.Invoke(currentHealth, maxHealth);
    }

    public void RegenHealth (int value)
    {
        if (isDead) return;

        health = Mathf.Min(health + value, maxHealth);

        UpdateHealthBar(health, maxHealth);
    }

    public void IncreaseMaxHP(int value)
    {
        maxHealth += value;   
        health += value;      
        UpdateHealthBar(health, maxHealth);
    }

    void ShowGameOverMenu()
    {
        GameOverMenuUI.Instance.Show();
    }

    public bool IsDead { get { return isDead; } }
    public bool IsHurt { get { return isHurt; } }
}
