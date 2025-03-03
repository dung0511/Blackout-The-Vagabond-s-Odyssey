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
            Debug.Log("player dealt: " + weapon.BulletDame);
            Destroy(gameObject);
        }
        
    }


}
