using Assets.Script.Boss.StoneGolem;
using System.Collections;
using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    public float flightTime;

    public Transform target;

    public GameObject projectile;
    public Transform firePos;
    public float projectileForce;
    public int projectileDame;

    public bool isFiring = false;
    public int countBullet = 0;

    public RangedWeaponSO rangedDetail;

    private StoneGolem stoneGolem;

    void Start()
    {
        stoneGolem = GetComponent<StoneGolem>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        projectileForce = rangedDetail.bulletForce;
        projectileDame = rangedDetail.damageRangedWeapon;
    }


    public void Fire()
    {
        isFiring = true;
        int fireType = Random.Range(1, 3);
        switch (fireType)
        {
            case 1:
                StartCoroutine(FireMultipleProjectiles(4, 0.75f));
                break;
            case 2:
                StartCoroutine(FireMultipleProjectilesRandomAngle(4, 0.75f));
                break;
        }

    }

    IEnumerator FireMultipleProjectilesRandomAngle(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            if (!stoneGolem.isDead)
            {
                float randomAngle = Random.Range(0f, 45f);
                firePos.rotation = Quaternion.Euler(0, 0, randomAngle);

                FireMultipleDirections();

                yield return new WaitForSeconds(delay);
            }

        }
        firePos.rotation = Quaternion.Euler(0, 0, 0);
        isFiring = false;
    }


    IEnumerator FireMultipleProjectiles(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            if (!stoneGolem.isDead)
            {
                countBullet++;
                FireSingleProjectile();
                yield return new WaitForSeconds(delay);
            }

        }
        if (countBullet >= count)
        {
            isFiring = false;
        }
    }

    void FireSingleProjectile()
    {

        GameObject bulletTmp = PoolManagement.Instance.GetBullet(projectile);
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

    void FireMultipleDirections()
    {

        float firePosAngle = firePos.rotation.eulerAngles.z;

        Vector2[] directions = new Vector2[]
        {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right,
        new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized,
        new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized
        };

        foreach (Vector2 dir in directions)
        {
            GameObject bulletTmp = PoolManagement.Instance.GetBullet(projectile);
            if (bulletTmp == null) continue;

            bulletTmp.transform.position = firePos.position;


            float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            float finalAngle = baseAngle + firePosAngle;


            bulletTmp.transform.rotation = Quaternion.Euler(0, 0, finalAngle);


            Vector2 rotatedDirection = new Vector2(
                Mathf.Cos(finalAngle * Mathf.Deg2Rad),
                Mathf.Sin(finalAngle * Mathf.Deg2Rad)
            );

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(rotatedDirection * projectileForce, ForceMode2D.Impulse);
        }
    }

}