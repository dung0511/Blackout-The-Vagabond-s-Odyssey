using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OrcWarlock : MonoBehaviour
{
    public EnemyDetailSO enemyDetails;

    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Room roomBelong;

    public int damage;
    public int health;
    public int maxHealth;

    public bool canAttacking = true;
    public bool isUsingSkill1 = false;

    public bool isDead = false;

    public float attackSpeed = 3f;
    public float turnOffSkill1Time;

    public Transform player;

    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;

    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int bulletCount = 3;
    public float spreadAngle = 30f;
    public int ShootTime;
    public float timeBtwShoot;

    bool isRotate = true;

    public int Skill3BulletCount;

    private void Awake()
    {
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();

    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //Laser laser = GetComponent<Laser>();
        //  laser.enabled = false;
        maxHealth = health;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canAttacking && !isDead)
        {
            animator.SetTrigger("isAttack");
            canAttacking = false;
        }
    }

    void performRangedAttack()
    {
        int attackType = Random.Range(1, 4);
        if (isUsingSkill1)
        {
            attackType = Random.Range(2, 4);
        }
        switch (attackType)
        {
            case 1:
                Skill1();
                break;
            case 2:
                Skill2();
                break;
            case 3:
                Skill3();
                break;

        }
        StartCoroutine(RangedAttackCooldown(attackSpeed));
    }
    IEnumerator RangedAttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canAttacking = true;
    }

    private void Skill1()
    {

        if (!canAttacking)
        {
            StartCoroutine(RotateBullet());
        }

    }

    private IEnumerator RotateBullet()
    {
        isRotate = true;
        isUsingSkill1 = true;
        foreach (Transform child in bulletPrefab1.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true); 
            }
        }

        StartCoroutine(Reset());

        while (isRotate)
        {
            bulletPrefab1.transform.Rotate(0f, 0f, 5f * Time.deltaTime * 60f);
            yield return null;
        }

        isUsingSkill1 = false;
        foreach (Transform child in bulletPrefab1.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }
        // bulletPrefab1.SetActive(false);
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(turnOffSkill1Time);
         isRotate = false;
    }

    public void Skill2()
    {
        StartCoroutine(StartSkill2());
    }

    private IEnumerator StartSkill2()
    {
        
        RotateToPlayerPos(firePoint, player);
        Vector2 baseDirection = firePoint.right;

        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int t = 0; t < ShootTime; t++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + angleStep * i;
                Vector2 shootDirection = Quaternion.Euler(0, 0, angle) * baseDirection;

                GameObject bullet = PoolManagement.Instance.GetBullet(bulletPrefab2,true);
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(shootDirection.normalized * bulletSpeed, ForceMode2D.Impulse);
            }

           
            yield return new WaitForSeconds(timeBtwShoot);
        }
    }


    private void RotateToPlayerPos(Transform objectToRotate, Transform player)
    {
        Vector3 direction = player.position - objectToRotate.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        objectToRotate.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void Skill3()
    {
        ShootBulletsInCircle(bulletPrefab2, gameObject.transform, Skill3BulletCount, bulletSpeed);
    }

    void ShootBulletsInCircle(GameObject bulletPrefab, Transform boss, int bulletCount, float bulletSpeed)
    {
        float angleStep = 360f / bulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {

            float radian = currentAngle * Mathf.Deg2Rad;
            Vector2 shootDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;


            // GameObject bullet = Instantiate(bulletPrefab, boss.position, Quaternion.identity);
            GameObject bullet = PoolManagement.Instance.GetBullet(bulletPrefab, true);
            bullet.GetComponent<OrcWarlockBulletHitBox>().OrcWarlock = this;
            bullet.transform.position = new Vector3(boss.position.x, boss.position.y + 1f, boss.position.z);
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(shootDirection.normalized * bulletSpeed, ForceMode2D.Impulse);
            }


            currentAngle += angleStep;
        }
    }

    
}
