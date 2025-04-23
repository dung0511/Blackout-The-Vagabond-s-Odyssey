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
        BossHealthBarController.Instance.UpdateSlider(stoneGolemHealth.health, stoneGolemHealth.maxHealth);
    }

    public void takeDame(int damage)
    {
        if(!stoneGolemHealth.isDead && !stoneGolemHealth.isImmune)
        {
            stoneGolemHealth.health -= damage;
            BossHealthBarController.Instance.UpdateSlider(stoneGolemHealth.health, stoneGolemHealth.maxHealth);
            if (stoneGolemHealth.health <= 0)
            {
                stoneGolemHealth.health = 0;
                stoneGolemHealth.isDead = true;
                stoneGolemHealth.GetComponent<StoneGolem>().animator.SetBool("isDead", true);
                stoneGolemHealth.GetComponent<CapsuleCollider2D>().enabled = false;
                stoneGolemHealth.GetComponent<BoxCollider2D>().enabled = false;
                stoneGolemHealth.GetComponent<CircleCollider2D>().enabled = false;
                stoneGolemHealth.GetComponent<StoneGolem_Movement_AI>().enabled = false;
                stoneGolemHealth.GetComponent<MovementToPosition>().enabled = false;
                stoneGolemHealth.GetComponent<ProjecttileManager>().enabled = false;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                foreach (var item in stoneGolemHealth.GetComponentsInChildren<BoxCollider2D>())
                {
                    item.enabled = false;
                }
                GameManager.Instance.UpdateBossKill();
                BossManager.Instance.bossKillEvent.Invoke();
            }
        }
        
        
    }

    public void HealthAfterImmune()
    {
        int percentHPHeal = Random.Range(10, 26);
        int healAmount = (stoneGolemHealth.maxHealth * percentHPHeal) / 100; 
        float healDuration = 5f;
        
        StartCoroutine(GradualHeal(healAmount, healDuration));
    }

    private IEnumerator GradualHeal(int totalHeal, float duration)
    {
        int healPerFrame = Mathf.CeilToInt((float)totalHeal / (duration / Time.deltaTime));
        int healedAmount = 0; 
        while (healedAmount < totalHeal)
        {
            
            int healThisFrame = Mathf.Min(healPerFrame, totalHeal - healedAmount);
            stoneGolemHealth.health = Mathf.Min(stoneGolemHealth.health + healThisFrame, stoneGolemHealth.maxHealth);
            healedAmount += healThisFrame;
            BossHealthBarController.Instance.UpdateSlider(stoneGolemHealth.health, stoneGolemHealth.maxHealth);
            yield return null; 
        }
    }


}
