using UnityEngine;

public class EnemyInteractZone : MonoBehaviour
{
    public Animator animator;
    private bool isTouchPlayer = false;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    void Update()
    {
        if (isTouchPlayer)
        {
            animator.SetBool("isAttack1", true);
        }
        else animator.SetBool("isAttack1", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveController player = collision.GetComponent<MoveController>();
        if (player != null)
        {
            isTouchPlayer = true;
            Debug.Log(isTouchPlayer);
        }
        //isTouchPlayer = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchPlayer = false;
        Debug.Log(isTouchPlayer);
    }
}
