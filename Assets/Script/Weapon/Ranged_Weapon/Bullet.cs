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
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        
        if (enemy != null)
        {
            
            enemy.takeDame(weapon.BulletDame);
          
            BulletPoolManagement.Instance.ReturnBullet(gameObject, weapon.bullet);
        }
        
    }


}
