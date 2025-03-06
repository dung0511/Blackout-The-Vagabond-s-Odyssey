using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private RangedWeapon weapon;
    
    private void Awake()
    {
         weapon = GameObject.Find("Weapon").GetComponentInChildren<RangedWeapon>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        // if (enemy != null)
        if(collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            
            enemy.takeDame(weapon.BulletDame);
          
            BulletPoolManagement.Instance.ReturnBullet(gameObject, weapon.bullet);
        }

        else BulletPoolManagement.Instance.ReturnBullet(gameObject, weapon.bullet);     
        //vuong: any collision obstacle, disable bullet
        // this.gameObject.SetActive(false);

    }


}
