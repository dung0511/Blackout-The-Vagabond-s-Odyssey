using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : Damageable
{
    public int maxHealth;
    private bool isHurt;
    private bool isDead;

    void Start()
    {
        isDead = false;
        health = GetComponent<Player>().health;
        isHurt = GetComponent<Player>().isHurt;
        isDead = GetComponent<Player>().isDead;
        maxHealth = health;
        UpdateHealthBar(maxHealth, maxHealth);
    }

    public override void takeDame(int dame)
    {
        if (isDead) return;
        if (GetComponent<PlayerArmorController>().TakeDamageArmor(dame) <= 0)
        {
            health -= dame;
            UpdateHealthBar(health, maxHealth);
            if (health <= 0)
            {
                isDead = true;
                health = 0;
                UpdateHealthBar(health, maxHealth);
                GameManager.Instance.SaveWhenDeadOrWin();
                StartCoroutine(ShowGameOverAfterDelay()); 
            }
            else
            {
                isHurt = true;
                StartCoroutine(ResetHurt());
            }
        }
        else
        {
            isHurt = true;
            StartCoroutine(ResetHurt());
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f); 
        GameOverMenuUI.Instance.Show();
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        UIManager.Instance.healthBarEvent.Invoke(currentHealth, maxHealth);
    }

    public void RegenHealth(float percent)
    {
        if (isDead) return;

        int healAmount = Mathf.RoundToInt(maxHealth * (percent / 100f));
        health = Mathf.Min(health + healAmount, maxHealth);
        UpdateHealthBar(health, maxHealth);
    }

    public void IncreaseMaxHP(int value)
    {
        maxHealth += value;
        UpdateHealthBar(health, maxHealth);
    }

    public bool IsDead { get { return isDead; } }
    public bool IsHurt { get { return isHurt; } }
}