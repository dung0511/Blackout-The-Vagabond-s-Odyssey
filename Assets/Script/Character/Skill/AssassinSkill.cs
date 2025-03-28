using System.Collections;
using UnityEngine;

public class AssassinSkill : BaseSkill
{
    public float CoolDownNormalSkill;
    public float CoolDownUltimateSkill;
    public int DameNormalSkill = 5;
  //  public int DameUltimateSkill;
    public Animator animator;

    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldownTime = 5f;

    public float ultimateCooldownTime;

    public LayerMask obstacleMask;

    public bool isDashing = false;
    public bool isUltimate = false;
    public bool canDash = true;
    public bool canUltimate = true;

    public WeaponController weaponController;
    private void Start()
    {
        //weaponController = GetComponent<WeaponController>();
        // animator = GetComponentInChildren<Animator>();
    }

    public override void NormalSkill()
    {
        if (!isDashing && canDash)
        {
            DameNormalSkill = 3;
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
        }
    }

    public override void UltimmateSkill()
    {
        if (!isUltimate && canUltimate)
        {
            DameNormalSkill = 10;
            isUltimate = true;
            StartCoroutine(UltimateCoolDown());
        }

    }

    public bool IsDashing { get { return isDashing; } }
    public bool IsUltimate { get { return isUltimate; } }
    public bool CanDash { get { return canDash; } }
    public bool CanUltimate { get { return canUltimate; } }

    IEnumerator Dash(Vector3 targetPos)
    {

        isDashing = true;
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
            
                enemy.takeDame(DameNormalSkill);
                Debug.Log("Dealth dame nomal:" + DameNormalSkill);
            
            

        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent<IDamageable>(out var enemy))
    //    {
    //        if (isDashing)
    //        {
    //            enemy.takeDame(DameNormalSkill);
    //            Debug.Log("Dealth dame nomal:" + DameNormalSkill);
    //        }
    //        else if (isUltimate)
    //        {
    //            enemy.takeDame(DameUltimateSkill);
    //            Debug.Log("Dealth dame ulti:" + DameUltimateSkill);
    //        }

    //    }
    //}

    public void SetActiveWeapon()
    {
        weaponController.gameObject.SetActive(true);
    }
    public void setActiveFalseWeapon()
    {
        weaponController.gameObject.SetActive(false);
    }

    public void SetAnimationUltimate()
    {
        isUltimate = false;
        animator.SetBool("isSkill2", false);
    }

    public void SetIsUltiamteTrue()
    {
        isUltimate = true;
    }

    public void SetAnimationDash()
    {
        isDashing = false;
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
        return isDashing || isUltimate;
    }
}
