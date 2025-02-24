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
            Debug.Log("is touch true");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && player.isDead)
        {
            isTouchPlayer = false;
            Debug.Log("is touch false");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchPlayer = false;
        Debug.Log("is touch false");
    }
}
