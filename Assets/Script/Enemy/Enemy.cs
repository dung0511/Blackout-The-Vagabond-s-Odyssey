using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float speed;
    public int health;
    public Animator animator;
    //private bool isTouchPlayer=false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (isTouchPlayer)
    //    {
    //        animator.SetBool("isAttack1", true);
    //    }
    //    else animator.SetBool("isAttack1", false);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    MoveController player = collision.GetComponent<MoveController>();
    //    if (player != null)
    //    {
    //        isTouchPlayer=true;
    //        Debug.Log(isTouchPlayer);
    //    }
    //    //isTouchPlayer = false;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    isTouchPlayer = false;
    //    Debug.Log(isTouchPlayer);
    //}
}
