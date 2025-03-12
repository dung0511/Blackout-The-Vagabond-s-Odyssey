using Assets.Script.Weapon.Throwable_Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ThrowableWeaponCollider : MonoBehaviour
{
    private ThrowableWeapon weapon;
    
    private void Awake()
    {
         weapon = GameObject.Find("Weapon").GetComponentInChildren<ThrowableWeapon>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        // if (enemy != null)
        if(collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {
            
            enemy.takeDame(weapon.BulletDame);
          
            PoolManagement.Instance.ReturnBullet(gameObject, weapon.bullet);
        }

        else PoolManagement.Instance.ReturnBullet(gameObject, weapon.bullet);     
        //vuong: any collision obstacle, disable bullet
        // this.gameObject.SetActive(false);

    }


}
