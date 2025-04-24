using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public RangedWeapon weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        // if (enemy != null)
        if(collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            AttackContext.CurrentAttackType = AttackType.Weapon;
            enemy.takeDame(weapon.dam);
          
            PoolManagement.Instance.ReturnBullet(gameObject, weapon.bulletPrefab);
        }

        else PoolManagement.Instance.ReturnBullet(gameObject, weapon.bulletPrefab);     
        //vuong: any collision obstacle, disable bullet
        // this.gameObject.SetActive(false);

    }


}
