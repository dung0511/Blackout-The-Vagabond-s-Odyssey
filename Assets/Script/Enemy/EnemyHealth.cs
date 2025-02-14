using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Enemy enemyHealth;
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
            enemyHealth.GetComponent<BoxCollider2D>().enabled = false;
            foreach (var item in enemyHealth.GetComponentsInChildren<BoxCollider2D>())
            {
                item.enabled = false;
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
}
