using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float speed;
    public int health;
    public Animator animator;
    public bool isHurt;
    //public bool isAttack1;

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
