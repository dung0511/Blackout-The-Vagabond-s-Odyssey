using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AssassinSkill : BaseSkill
{
    public float CoolDownNormalSkill;
    public float CoolDownUltimateSkill;
    public int DameNormalSkill = 5;
    public int DameUltimateSkill;
    private int damage;

    public Animator animator;

    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldownTime = 5f;

    public float ultimateCooldownTime;

    public LayerMask obstacleMask;

    public bool isUsingNormal = false;
    public bool isUsingUltimate = false;
    public bool canDash = true;
    public bool canUltimate = true;

    public Light2D light;
    public float lightdefault;
    public float buffed;


    public PlayerWeaponController weaponController;
    private void Start()
    {
        //weaponController = GetComponent<WeaponController>();
        // animator = GetComponentInChildren<Animator>();
    }

    public override void NormalSkill()
    {

        damage = DameNormalSkill;
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector3 dashDirection = (mouseWorldPos - transform.root.position).normalized;

        Vector2 startPos = transform.root.position;
        RaycastHit2D hit = Physics2D.Raycast(startPos, dashDirection, dashDistance, obstacleMask);

        Vector3 targetPos;
        if (hit.collider != null)
        {
            targetPos = hit.point - (Vector2)dashDirection * 0.1f;
        }
        else
        {
            targetPos = transform.root.position + dashDirection * dashDistance;
        }

        StartCoroutine(Dash(targetPos));
        StartCoroutine(DashCooldown());

        //bat dau cooldown
        SkillCooldownUI.Instance.TriggerCooldown_E(CoolDownNormalSkill);
    }

    public override void UltimmateSkill()
    {
        light.GetComponent<Light2D>().pointLightOuterRadius = buffed;
        damage = DameUltimateSkill;
        isUsingUltimate = true;
        StartCoroutine(UltimateCoolDown());

        //bat dau cooldown
        SkillCooldownUI.Instance.TriggerCooldown_Q(CoolDownUltimateSkill);
    }

    public void SetLight()
    {
        light.GetComponent<Light2D>().pointLightOuterRadius = lightdefault;
    }

    IEnumerator Dash(Vector3 targetPos)
    {


        Vector3 startPos = transform.root.position;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.root.position = Vector3.Lerp(startPos, targetPos, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.root.position = targetPos;
        //isDashing = false;
        //animator.SetBool("isSkill1", false);
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }



    IEnumerator UltimateCoolDown()
    {
        canUltimate = false;
        yield return new WaitForSeconds(ultimateCooldownTime);
        canUltimate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
        {

            enemy.takeDame(damage);
            Debug.Log("Dealth dame nomal:" + damage);



        }
    }

    public override void SetActiveWeapon()
    {
        weaponController.gameObject.SetActive(true);
    }
    public override void setActiveFalseWeapon()
    {
        weaponController.gameObject.SetActive(false);
    }

    public void SetAnimationUltimate()
    {
        animator.SetBool("isSkill2", false);
    }

    public void SetIsUltiamteTrue()
    {
        isUsingUltimate = true;
    }

    public void SetAnimationDash()
    {
        animator.SetBool("isSkill1", false);
    }

    public override bool CanUseSkill1()
    {
        return canDash;
    }

    public override bool CanUseSkill2()
    {
        return canUltimate;
    }

    public override bool IsUsingSkill()
    {
        return isUsingNormal || isUsingUltimate;
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
