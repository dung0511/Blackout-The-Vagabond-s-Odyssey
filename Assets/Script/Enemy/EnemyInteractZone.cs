using UnityEngine;

public class EnemyInteractZone : MonoBehaviour
{
    public Animator animator;
    private bool isTouchPlayer ;
    void Start()
    {
        isTouchPlayer = false;
        animator = GetComponentInParent<Animator>();
    }
    void Update()
    {
       
            animator.SetBool("isAttack1", isTouchPlayer);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveController player = collision.GetComponent<MoveController>();
        if (player != null)
        {
            isTouchPlayer = true;
           
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchPlayer = false;
        
    }
}
