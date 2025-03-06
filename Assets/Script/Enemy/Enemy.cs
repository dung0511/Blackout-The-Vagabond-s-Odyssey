using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDetailSO enemyDetails;


    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    public int damage;
    //public float speed;
    public int health;
    [HideInInspector] public Animator animator;
    public bool isHurt;
    public bool has3Attack;
    //public bool isAttack1;


    private bool isAttacking = false;
 
    public float attackSpeed = 1f;
    [HideInInspector] public Room roomBelong;
    private void Awake()
    {

        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();

    }
    void Start()
    {
        // isAttack1 = false;
        animator = GetComponent<Animator>();
        isHurt = false;
    }

    void Update()
    {
        //animator.SetBool("isAttack1", checkAttack());
        if (!isAttacking && checkAttack()) performAttack();
        else ResetAllAttackParameters();
        isHurt=GetComponent<EnemyHealth>().isHurt;
        animator.SetBool("isHurt", isHurt);
        
    }
    void performAttack()
    {
        isAttacking = true;
        int attackType = has3Attack ? Random.Range(1, 4) : Random.Range(1, 3);
        ResetAllAttackParameters();
        //Debug.Log(attackType);
        switch (attackType)
        {
            case 1:
                animator.SetBool("isAttack1", true);
                break;
            case 2:
                animator.SetBool("isAttack2", true);
                break;
            case 3:
                animator.SetBool("isAttack3", true);
                break;
        }
        StartCoroutine(AttackCooldown(attackSpeed));
    }

    void ResetAllAttackParameters()
    {
        animator.SetBool("isAttack1", false);
        animator.SetBool("isAttack2", false);
        animator.SetBool("isAttack3", false);
    }
    IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isAttacking = false;
        ResetAllAttackParameters();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //Bullet bullet = collision.GetComponent<Bullet>();
    //if (bullet != null)
    //{
    //    isHurt = true;
    //    Debug.Log("Is hurt is true");
    //    StartCoroutine(ResetHurt());


    //    Destroy(collision.gameObject);
    //}
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Bullet bullet = collision.collider.GetComponent<Bullet>();
    //    if (bullet != null)
    //    {
    //        isHurt = true;
    //        Debug.Log("Is hurt is true");
    //        StartCoroutine(ResetHurt());


    //        Destroy(collision.gameObject);
    //    }
    //}

    private bool checkAttack()
    {
        return GetComponentInChildren<EnemyInteractZone>().isTouchPlayer;
    }

    
}
