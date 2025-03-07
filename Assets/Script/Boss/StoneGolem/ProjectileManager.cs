using System.Collections;
using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    public float flightTime;

    public Transform target;

    public GameObject projectile;
    public Transform firePos;
    public float TimeBtwFire = 0.2f;
    public float projectileForce;
    public int projectileDame;
    private float lastFireTime = 0;

    public RangedWeaponSO rangedDetail;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        TimeBtwFire = rangedDetail.TimeBtwFire;
        projectileForce = rangedDetail.bulletForce;
        projectileDame = rangedDetail.damageRangedWeapon;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Fire();
        }
    }

    public void Fire()
    {

        float elapsedTime = Time.time - lastFireTime;
        if (elapsedTime >= TimeBtwFire)
        {
            lastFireTime = Time.time;
            GameObject bulletTmp = BulletPoolManagement.Instance.GetBullet(projectile);
            if (bulletTmp == null) return;

            bulletTmp.transform.position = firePos.position;     
            Vector2 direction = (Vector2)(target.position - firePos.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           
            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;

          
            Vector2 shootDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            rb.AddForce(shootDirection * projectileForce, ForceMode2D.Impulse);
        }
    }
}
