using Assets.Script.Boss.StoneGolem;
using UnityEngine;

public class OrcWarlockHealth : MonoBehaviour, IDamageable
{
    [HideInInspector] public OrcWarlock orcWarlockHealth;
    public bool isHurt;

    void Start()
    {
        
        orcWarlockHealth = GetComponent<OrcWarlock>();
    }

    public void takeDame(int damage)
    {
        if (!orcWarlockHealth.isDead)
        {
            orcWarlockHealth.health -= damage;
            if (orcWarlockHealth.health <= 0)
            {
                orcWarlockHealth.health = 0;
                orcWarlockHealth.isDead = true;
                orcWarlockHealth.GetComponent<OrcWarlock>().animator.SetTrigger("isDead");
                orcWarlockHealth.GetComponent<CapsuleCollider2D>().enabled = false;
                orcWarlockHealth.GetComponent<BoxCollider2D>().enabled = false;
                orcWarlockHealth.GetComponent<OrcWarlock_Movement_AI>().enabled = false;
                orcWarlockHealth.GetComponent<MovementToPosition>().enabled = false;
                orcWarlockHealth.bulletPrefab1.SetActive(false);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                foreach (var item in orcWarlockHealth.GetComponentsInChildren<BoxCollider2D>())
                {
                    item.enabled = false;
                }
                GameManager.Instance.UpdateBossKill();
                BossManager.Instance.bossKillEvent.Invoke();
            }
        }


    }
}
