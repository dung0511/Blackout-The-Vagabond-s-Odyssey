using System.Collections;
using UnityEngine;

public class AssassinSkill : MonoBehaviour, ISkillController
{
    public float CoolDownNormalSkill;
    public float CoolDownUltimateSkill;
    public int DameNormalSkill;
    public int DameUltimateSkill;
    public Animator animator;


    public float dashDistance = 5f;

    public float dashDuration = 0.1f;

    public LayerMask obstacleMask;


    private bool isDashing = false;

    private bool isUltimate = false;

    private void Start()
    {
       // animator = GetComponentInChildren<Animator>();
    }
    public void NormalSkill()
    {
        animator.SetBool("isSkill1", true);
        Vector3 mouseScreenPos = Input.mousePosition;

        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);


        Vector3 dashDirection = (mouseWorldPos - transform.position).normalized;


        Vector2 startPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(startPos, dashDirection, dashDistance, obstacleMask);

        Vector3 targetPos;
        if (hit.collider != null)
        {

            targetPos = hit.point - (Vector2)dashDirection * 0.1f;
        }
        else
        {

            targetPos = transform.position + dashDirection * dashDistance;
        }
        //animator.SetBool("isSkill1", false);
        StartCoroutine(Dash(targetPos));
    }

    public void UltimmateSkill()
    {
       
    }

    public bool IsDashing { get { return isDashing; } }
    public bool IsUltimate { get {  return isUltimate; } }

    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.E) && !isDashing)
    //    {

    //    }
    //    if (Input.GetKeyDown(KeyCode.Q) && !isUltimate)
    //    {
    //        animator.SetBool("isSkill2", true);
    //        StartCoroutine(Ultimate());
    //    }
    //}



    IEnumerator Ultimate()
    {
        isUltimate = true;
        Vector3 startPos = transform.position;
       // float elapsedTime = 0f;


      //  {
         //   transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / dashDuration);
           // elapsedTime += Time.deltaTime;
            yield return 0.25f;
        // }

        //transform.position = targetPos;
        isUltimate = false;
        animator.SetBool("isSkill2", false);
    }

    IEnumerator Dash(Vector3 targetPos)
    {
        isDashing = true;
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

      
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isDashing = false;
        animator.SetBool("isSkill1", false);
    }
    void SetSkillAnimation()
    {
        animator.SetBool("isSkill1", false);
    }
}
