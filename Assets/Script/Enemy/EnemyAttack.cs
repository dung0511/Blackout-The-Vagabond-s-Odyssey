using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        //Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            // them script kiem tra xem quai chet chua 
            //isTouchPlayer = true;
            player.healthController.takeDame(GetComponentInParent<Enemy>().damage);
           
        }
        //isTouchPlayer = false;
    }
}
