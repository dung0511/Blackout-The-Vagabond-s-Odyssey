using System.Collections;
using UnityEngine;

public class KnightSkill : BaseSkill
{
    public Animator animator;
    public Animator shieldAnimator;
    public GameObject clonePrefab;
    public float cloneCoolDown = 10f;
    public int NumberSpawn = 3;

    public float ultimateCooldown = 10f;
    public LayerMask obstacleMask;

    public float normalCoolDown = 5f;
    public float shieldCoolDown = 7f;
    public int shieldDame = 5;

    public bool isUsingNormal = false;
    public bool isUsingUltimate = false;
    public bool canUseNormal = true;
    public bool canUseUltimate = true;

    public WeaponController weaponController;


    private void Start()
    {
        clonePrefab.GetComponent<CloneKnight>().knightSkill = this;
        animator = GetComponent<Animator>();

    }
    public override bool CanUseSkill1()
    {
        return canUseNormal;
    }

    public override bool CanUseSkill2()
    {
        return canUseUltimate;
    }

    public override bool IsUsingSkill()
    {
        return isUsingNormal || isUsingUltimate;
    }

    //public override void NormalSkill()
    //{
    //    canUseNormal = false;
    //    animator.SetBool("isSkill1", false);
    //    float radius = 2f;
    //    Vector3 center = transform.root.position; 

    //    float angle = Random.Range(0f, 2f * Mathf.PI);

    //    Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

    //    GameObject cloneKnight = PoolManagement.Instance.GetBullet(clonePrefab);
    //    cloneKnight.transform.position = center + spawnPosition;
    //    StartCoroutine(ResetNormalSkill());
    //}

    public override void NormalSkill()
    {
        //isUsingNormal = true; 
        canUseNormal = false;
        animator.SetBool("isSkill1", false);
        shieldAnimator.SetTrigger("isUsingShield");
        transform.root.GetComponent<PlayerArmorController>().ShieldSkill();
        StartCoroutine(ResetShield());
        
    }


    IEnumerator ResetNormalSkill()
    {

        yield return new WaitForSeconds(normalCoolDown);
         canUseNormal = true;
    }

    IEnumerator ResetShield()
    {
        Debug.Log("shield cool down: " + shieldCoolDown);
        yield return new WaitForSeconds(shieldCoolDown);
        shieldAnimator.SetTrigger("isShieldDone");
        transform.root.GetComponent<PlayerArmorController>().EndShieldSkill();
        StartCoroutine(ResetNormalSkill());
    }

    IEnumerator ResetUltimateSkill()
    {
        yield return new WaitForSeconds(ultimateCooldown);
        canUseUltimate = true;
    }

    public override void UltimmateSkill()
    {
       
        canUseUltimate = false;
        animator.SetBool("isSkill2", false);
        float radius = 2f;
        Vector3 center = transform.position;
        int maxAttempts = 10;

        Vector3 spawnPosition = center;
        for (int spawn = 0; spawn < NumberSpawn; spawn++)
        {
            for (int i = 0; i < maxAttempts; i++)
            {
                float angle = Random.Range(0f, 2f * Mathf.PI);
                Vector3 tempPosition = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;


                if (!Physics2D.OverlapCircle(tempPosition, 0.5f, obstacleMask))
                {
                    spawnPosition = tempPosition;

                    break;
                }
            }
            GameObject cloneKnight = PoolManagement.Instance.GetBullet(clonePrefab);
            cloneKnight.transform.position = spawnPosition;
        }
        StartCoroutine(ResetUltimateSkill());
    }

    public override void SetActiveWeapon()
    {
        weaponController.gameObject.SetActive(true);
    }
    public override void setActiveFalseWeapon()
    {
        weaponController.gameObject.SetActive(false);
    }
    public override void SetIsUsingNormalFalse()
    {
        isUsingNormal = false;
    }
    public override void SetIsUsingUltimateFalse()
    {
        isUsingUltimate = false;
    }

    public override void SetIsUsingUltimate()
    {
        isUsingUltimate = true;
    }

    public override void SetIsUsingNormal()
    {
        isUsingNormal = true;
    }
}
