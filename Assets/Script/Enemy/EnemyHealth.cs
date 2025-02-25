using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public Enemy enemyHealth;
    public bool isHurt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHurt = false;
        enemyHealth=GetComponent<Enemy>();
    }
    
    public void takeDame(int damage)
    {
        enemyHealth.health-= damage;


        if (enemyHealth.health <= 0)
        {
            enemyHealth.health = 0;
            enemyHealth.GetComponent<Enemy>().animator.SetBool("isDead", true);
            enemyHealth.GetComponent<CapsuleCollider2D>().enabled = false;
            enemyHealth.GetComponent<Enemy_Movement_AI>().enabled = false;
            enemyHealth.GetComponent<MovementToPosition>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            foreach (var item in enemyHealth.GetComponentsInChildren<CapsuleCollider2D>())
            {
                item.enabled = false;
            }

        }
        else
        {
            isHurt = true;
            Debug.Log(isHurt + " hurt");
            enemyHealth.GetComponent<Enemy>().animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurt());
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
}
