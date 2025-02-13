using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public WeaponManagemnt weaponManager;
    public Weapon bulletDamage;
    public GameObject currentWeapon;
    //public Transform wallTransform;
    void Start()
    {
        GameObject player= GameObject.Find("Player");
        weaponManager = player.GetComponentInChildren<WeaponManagemnt>();
        currentWeapon = weaponManager.currentWeapon;
        bulletDamage= currentWeapon.GetComponent<Weapon>();
        Debug.Log("Check:"+ currentWeapon.name);
        //GameObject dungeonLayout = GameObject.Find("Dungeon Layout");

        //if (dungeonLayout != null)
        //{
        //    Transform wallTransform = dungeonLayout.transform.Find("Wall");
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null) 
        {
            enemy.takeDame(bulletDamage.BulletDame);
            Debug.Log("player dealt: "+ bulletDamage.BulletDame);
            Destroy(gameObject);
        }
        
        
    }
}
