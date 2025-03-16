using Assets.Script;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public RangedEnemyProjectile arrow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
           
            player.healthController.takeDame(arrow.projectileDame);
            

            PoolManagement.Instance.ReturnBullet(gameObject, arrow.projectile);
        }

        else PoolManagement.Instance.ReturnBullet(gameObject, arrow.projectile);
    }


}
