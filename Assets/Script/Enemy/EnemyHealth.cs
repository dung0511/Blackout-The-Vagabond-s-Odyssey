using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Enemy enemyHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth=GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isHurt)
        //{
        //    enemyHealth.GetComponent<Enemy>().animator.SetBool("isHurt", true);
        //}
        //enemyHealth.GetComponent<Enemy>().animator.SetBool("isHurt", false);
    }
    public void takeDame(int damage)
    {
        enemyHealth.health-= damage;
        
       
        if (enemyHealth.health <= 0 )
        {
            enemyHealth.health = 0;
            enemyHealth.GetComponent<Enemy>().animator.SetBool("isDead",true);
            enemyHealth.GetComponent<BoxCollider2D>().enabled=false;
            foreach (var item in enemyHealth.GetComponentsInChildren<BoxCollider2D>())
            {
                item.enabled = false;
            }
            
        }
    }
}
