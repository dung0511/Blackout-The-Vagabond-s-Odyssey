using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [HideInInspector] public Enemy enemyHealth;
    public bool isHurt;
    public bool isDead=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead = false; 
        isHurt = false;
        enemyHealth=GetComponent<Enemy>();
    }
    
    public void takeDame(int damage)
    {

        //enemyHealth.health-= damage;
        //if (enemyHealth.health <= 0 && !isDead)
        //{
        //    isDead = true;
        //    GameManager.Instance.UpdateEnemyKilled();
        //    GameManager.Instance.UpdateWeaponUsed();
        //    enemyHealth.health = 0;
        //    enemyHealth.GetComponent<Enemy>().animator.SetBool("isDead", true);
        //    enemyHealth.GetComponent<CapsuleCollider2D>().enabled = false;
        //    enemyHealth.GetComponent<CircleCollider2D>().enabled = false;
        //    enemyHealth.GetComponent<BoxCollider2D>().enabled = false;
        //    enemyHealth.GetComponent<Enemy_Movement_AI>().enabled = false;
        //    enemyHealth.GetComponent<MovementToPosition>().enabled = false;
        //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //    rb.linearVelocity = Vector2.zero;
        //    rb.angularVelocity = 0f;
        //    foreach (var item in enemyHealth.GetComponentsInChildren<BoxCollider2D>())
        //    {
        //        item.enabled = false;
        //    }
        //    enemyHealth.roomBelong.OnEnemyDeath();
        //}
        //else
        //{
        //    isHurt = true;
        //    enemyHealth.GetComponent<Enemy_Movement_AI>().StartChase();
        //    StartCoroutine(ResetHurt());
        //}
        AttackType currentType = AttackContext.CurrentAttackType;
        HandleDamage(damage, currentType);
    }

    private void HandleDamage(int damage, AttackType attackType)
    {
        enemyHealth.health -= damage;
        if (enemyHealth.health <= 0 && !isDead)
        {
            isDead = true;
            GameManager.Instance.UpdateEnemyKilled();
            enemyHealth.health = 0;
            enemyHealth.GetComponent<Animator>().SetBool("isDead", true);
            
            enemyHealth.roomBelong.OnEnemyDeath();
        }
        else
        {
            isHurt = true;
            
            if (attackType == AttackType.Skill)
            {
                Debug.Log("Enemy attacked by skill!");
            
            }
            else
            {
               //GameManager.Instance.UpdateEnemyKilled();
                Debug.Log("Enemy attacked by weapon!");
            }
            enemyHealth.GetComponent<Enemy_Movement_AI>().StartChase();
            StartCoroutine(ResetHurt());
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
}
