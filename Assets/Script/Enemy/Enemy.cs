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
    //public bool isAttack1;

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
        animator.SetBool("isAttack1", checkAttack());
        isHurt=GetComponent<EnemyHealth>().isHurt;
        animator.SetBool("isHurt", isHurt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Bullet bullet = collision.GetComponent<Bullet>();
        //if (bullet != null)
        //{
        //    isHurt = true;
        //    Debug.Log("Is hurt is true");
        //    StartCoroutine(ResetHurt());

            
        //    Destroy(collision.gameObject);
        //}
    }

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
