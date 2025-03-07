using Assets.Script.Boss.StoneGolem;
using System.Collections;
using UnityEngine;

public class StoneGolemHealth : MonoBehaviour, IDamageable
{
    [HideInInspector] public StoneGolem stoneGolemHealth;
    public bool isHurt;
    void Start()
    {
        isHurt = false;
        stoneGolemHealth = GetComponent<StoneGolem>();
    }

    public void takeDame(int damage)
    {
        stoneGolemHealth.health -= damage;
        if (stoneGolemHealth.health <= 0)
        {
            stoneGolemHealth.health = 0;
            stoneGolemHealth.GetComponent<StoneGolem>().animator.SetBool("isDead", true);
            stoneGolemHealth.GetComponent<CapsuleCollider2D>().enabled = false;
            stoneGolemHealth.GetComponent<BoxCollider2D>().enabled = false;
            stoneGolemHealth.GetComponent<Enemy_Movement_AI>().enabled = false;
            stoneGolemHealth.GetComponent<MovementToPosition>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            foreach (var item in stoneGolemHealth.GetComponentsInChildren<BoxCollider2D>())
            {
                item.enabled = false;
            }
            //stoneGolemHealth.roomBelong.OnEnemyDeath();
        }
        
    }

    
}
