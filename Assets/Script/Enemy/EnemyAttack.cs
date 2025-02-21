using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            // them script kiem tra xem quai chet chua 
            //isTouchPlayer = true;
            player.takeDame(GetComponentInParent<Enemy>().damage);
            Debug.Log("dealt:"+ GetComponentInParent<Enemy>().damage);
        }
        //isTouchPlayer = false;
    }
}
