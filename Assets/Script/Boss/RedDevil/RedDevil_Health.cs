using System.Collections;
using UnityEngine;

public class RedDevil_Health : MonoBehaviour, IDamageable
{
    [HideInInspector] public RedDevil enemyHealth;
    public bool isHurt;
    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead = false;
        isHurt = false;
        enemyHealth = GetComponent<RedDevil>();
        BossHealthBarController.Instance.UpdateSlider(enemyHealth.health, enemyHealth.maxHealth);
    }

    public void takeDame(int damage)
    {
        enemyHealth.health -= damage;
        BossHealthBarController.Instance.UpdateSlider(enemyHealth.health, enemyHealth.maxHealth);
        if (enemyHealth.health <= 0 && !isDead)
        {
            isDead = true;
            enemyHealth.health = 0;
            enemyHealth.GetComponent<RedDevil>().animator.SetBool("isDead", true);
            enemyHealth.GetComponent<CapsuleCollider2D>().enabled = false;
            enemyHealth.GetComponent<CircleCollider2D>().enabled = false;
            enemyHealth.GetComponent<BoxCollider2D>().enabled = false;
            enemyHealth.GetComponent<RedDevil_Movement>().enabled = false;
            enemyHealth.GetComponent<MovementToPosition>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            foreach (var item in enemyHealth.GetComponentsInChildren<BoxCollider2D>())
            {
                item.enabled = false;
            }
            GameManager.Instance.UpdateBossKill();
            BossManager.Instance.bossKillEvent.Invoke();
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
}
