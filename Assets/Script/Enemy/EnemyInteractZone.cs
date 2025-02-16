using UnityEngine;

public class EnemyInteractZone : MonoBehaviour
{
    //public Animator animator;
    public bool isTouchPlayer ;
    void Start()
    {
        isTouchPlayer = false;
       // animator = GetComponentInParent<Animator>();
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
