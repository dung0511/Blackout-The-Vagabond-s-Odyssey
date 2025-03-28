using System.Collections;
using UnityEngine;

public class KnightSkill : BaseSkill
{
   
   // public int DameNormalSkill = 5;
    public int DameUltimateSkill;
    public Animator animator;

    public GameObject clonePrefab;
    public float cloneCoolDown=10f;

    public float ultimateCooldown=10f;
    public float normalCoolDown=5f;
    //public LayerMask obstacleMask;



    public bool isNormal = false;
    public bool isUltimate = false;
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
        return isNormal || isUltimate;
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
        animator.SetBool("isSkill1", false);
        float radius = 2f; 
        Vector3 center = transform.position;
        LayerMask obstacleMask = LayerMask.GetMask("Background"); 
        int maxAttempts = 10; 

        Vector3 spawnPosition = center;
        bool validPosition = false;

        for (int i = 0; i < maxAttempts; i++)
        {
            float angle = Random.Range(0f, 2f * Mathf.PI);
            Vector3 tempPosition = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

           
            if (!Physics2D.OverlapCircle(tempPosition, 0.5f, obstacleMask))
            {
                spawnPosition = tempPosition;
                validPosition = true;
                break;
            }
        }

        if (!validPosition)
        {
            Debug.LogWarning("position clone invalid!");
            return;
        }

        GameObject cloneKnight = PoolManagement.Instance.GetBullet(clonePrefab);
        cloneKnight.transform.position = spawnPosition;
        StartCoroutine(ResetNormalSkill());
    }

    IEnumerator ResetNormalSkill()
    {
        canUseNormal=false;
        yield return normalCoolDown;
        canUseNormal=true;
    }

    IEnumerator ResetUltimateSkill()
    {
        canUseUltimate=false;
        yield return ultimateCooldown;
        canUseUltimate = true;
    }

    public override void UltimmateSkill()
    {
        ResetUltimateSkill();
    }

    public void SetActiveWeapon()
    {
        weaponController.gameObject.SetActive(true);
    }
    public void setActiveFalseWeapon()
    {
        weaponController.gameObject.SetActive(false);
    }
}
