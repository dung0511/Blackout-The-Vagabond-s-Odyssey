using Assets.Script.UI.Inventory;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjecttileManager projecttileManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthController player = collision.GetComponent<PlayerHealthController>();
        if (player!=null)
        {

            player.takeDame(projecttileManager.projectileDame);

            PoolManagement.Instance.ReturnBullet(gameObject, projecttileManager.projectile);
        }

        else PoolManagement.Instance.ReturnBullet(gameObject, projecttileManager.projectile);
    }


}
