using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class WorldBoss : MonoBehaviour, IDamageable
{
    //public float skillCoolDown;
    public int health;
    public int maxHealth;
    private Animator animator;
    public bool isPerformAtatck = false;
    public bool isPlayerStandingClose = false;
    private bool isDead=false;

    //skill1
    public List<GameObject> ListSpawnNormal;
    public List<GameObject> ListSpawnElite;
    public int enemySpawn;
    public LayerMask obstacleMask;

    // skill2
    private GameObject player;
    private GameObject light;
    public float ResetLightCoolDown;
    private GameObject[] pedestalObjects;

    //skill3
    public float pullStrength = 10f;
    public float pullDuration = 5f;
    public GameObject hitBox;
    public int Skill3Damage;
    //skill4

    //skill5
    public GameObject effect;
    public int Skill5Damage;

    //TP
    public GameObject bulletPrefab;
    public int NumberOfBullet;
    public int Skill6Damage;
    public float bulletSpeed;
    void Start()
    {
        isDead = false;
        hitBox = transform.Find("Skill3HitBox").gameObject;
        isPerformAtatck = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        light = player.transform.Find("Light").gameObject.GetComponentInChildren<Light2D>().gameObject;
        pedestalObjects = GameObject.FindGameObjectsWithTag("Pedestal");
        maxHealth=health;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPerformAtatck) return;
        if (isPlayerStandingClose)
        {
            int close = Random.Range(1, 3);
            switch (close)
            {
                case 0:
                    isPerformAtatck = true;
                    animator.SetTrigger("BossSkill5");
                    break;
                case 1:
                    isPerformAtatck = true;
                    animator.SetTrigger("BossSkill3");
                    break;
            }

        }
        int num = Random.Range(1, 5);
        switch (num)
        {
            case 1:
                isPerformAtatck = true;
                animator.SetTrigger("BossSkill1");
                break;
            case 2:
                isPerformAtatck = true;
                animator.SetTrigger("BossSkill2");
                break;
            case 3:
                isPerformAtatck = true;
                animator.SetTrigger("BossSkill3");
                break;
            case 4:
                isPerformAtatck = true;
                animator.SetTrigger("BossSkill6");
                break;
        }
    }

    public void SetPerformAtatck()
    {
        isPerformAtatck = false;
    }

    public void Skill1()
    {

        float radius = 2f;
        Vector3 center = transform.position;
        int maxAttempts = 10;

        Vector3 spawnPosition = center;
        for (int spawn = 0; spawn < enemySpawn; spawn++)
        {
            for (int i = 0; i < maxAttempts; i++)
            {
                float angle = Random.Range(0f, 4f * Mathf.PI);
                Vector3 tempPosition = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;


                if (!Physics2D.OverlapCircle(tempPosition, 0.5f, obstacleMask))
                {
                    spawnPosition = tempPosition;

                    break;
                }
            }
            if (health <= (maxHealth * 0.5f))
            {
                Instantiate(ListSpawnNormal[Random.Range(0, ListSpawnNormal.Count)], spawnPosition, Quaternion.identity);
            }
            else Instantiate(ListSpawnNormal[Random.Range(0, ListSpawnElite.Count)], spawnPosition, Quaternion.identity);

            //enemy.transform.position = spawnPosition;
            // cloneKnight.GetComponent<CloneKnight>().SetWeaponForClone(weaponController.baseWeapon.gameObject);
        }
        // StartCoroutine(ResetUltimateSkill());
    }

    public void SKill2()
    {
        light.SetActive(false);
        foreach (GameObject obj in pedestalObjects)
        {
            obj.GetComponentInChildren<Light2D>().enabled=false;
        }
        StartCoroutine(ResetTurnOffLightPlayer());
    }

    public void Skill3()
    {
        PullPlayer(player.GetComponent<Rigidbody2D>(), gameObject.transform, pullStrength, pullDuration);

    }

    IEnumerator ResetTurnOffLightPlayer()
    {
        yield return new WaitForSeconds(ResetLightCoolDown);
        Debug.Log("chay di dmm");
        light.SetActive(true);
        Debug.Log(pedestalObjects.Count());
        foreach (GameObject obj in pedestalObjects)
        {
            obj.GetComponentInChildren<Light2D>().enabled = true;
        }
    }

    IEnumerator PullPlayerCoroutine(Rigidbody2D playerRb, Transform boss, float pullStrength, float pullDuration)
    {
        hitBox.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < pullDuration)
        {
            Vector2 direction = ((Vector2)boss.position - playerRb.position).normalized;
            playerRb.AddForce(direction * pullStrength, ForceMode2D.Force);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hitBox.SetActive(false);
    }

    public void PullPlayer(Rigidbody2D playerRb, Transform boss, float pullStrength, float pullDuration)
    {
        StartCoroutine(PullPlayerCoroutine(playerRb, boss, pullStrength, pullDuration));
    }

    public void Skill5()
    {

        effect.GetComponent<Animator>().SetTrigger("explode");
        StartCoroutine(TurnOffEffect());

    }

    IEnumerator TurnOffEffect()
    {
        yield return new WaitForSeconds(0.4f);

    }

    public void TP()
    {
        gameObject.transform.position = player.transform.position;
    }
    public void TPout()
    {
        ShootBulletsInCircle(bulletPrefab, gameObject.transform, NumberOfBullet, bulletSpeed);
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
            bullet.transform.position = new Vector3(boss.position.x, boss.position.y + 4.5f, boss.position.z);
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletSpeed;
            }


            currentAngle += angleStep;
        }
    }

    public void takeDame(int damage)
    {
        health-=damage;
        BossHealthBarController.Instance.UpdateSlider(health, maxHealth);
        if (health <= 0 && !isDead)
        {
            isDead = true;
            health = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled=false;
            animator.SetTrigger("isDead");
            GameManager.Instance.UpdateBossKill();
           // DontDestroyCleaner.ClearDDOL();
            //BossManager.Instance.bossKillEvent.Invoke();
            StartCoroutine(LoadEndGameCutSceneWhenFinishGame());
        }
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
    IEnumerator LoadEndGameCutSceneWhenFinishGame()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("CutsceneEndGame");
    }
}
