using UnityEngine;

public class EnemyInteractZone : MonoBehaviour
{
    public bool isTouchPlayer = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && !player.isDead)
        {
            isTouchPlayer = true;
           
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && player.isDead)
        {
            isTouchPlayer = false;
            
        }
    }

    private void OnTriggerExit2D()
    {
        isTouchPlayer = false;
        
    }
}
