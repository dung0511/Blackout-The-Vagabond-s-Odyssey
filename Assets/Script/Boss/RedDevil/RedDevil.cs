using System.Collections;
using UnityEngine;

public class RedDevil : MonoBehaviour
{
    public EnemyDetailSO enemyDetails;

    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Room roomBelong;


    public int damage;
    public int health;
    public int maxHealth;

    private bool isAttacking = false;
    public bool isHurt;
   // public float attackSpeed = 1f;

   
    private int currentComboStep = 1;
    public float comboResetTime = 2f;
    private float comboTimer = 0f;

    private void Awake()
    {

        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        maxHealth = health;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        isHurt = false;
    }

    private bool checkAttack()
    {
        return GetComponentInChildren<EnemyInteractZone>().isTouchPlayer;
    }
    void Update()
    {
        
        if (!isAttacking && checkAttack())
        {
            performComboAttack();
            comboTimer = 0f; // reset timer combo khi attack
        }
        else
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= comboResetTime)
            {
                currentComboStep = 1; // reset combo nếu không attack sau 1 khoang thoi gian
                comboTimer = 0f;
                
            }
            //ResetAllAttackParameters();
        }
        isHurt = GetComponent<RedDevil_Health>().isHurt;                            
        animator.SetBool("isHurt", isHurt);
        animator.SetBool("playerInRange", checkAttack());

        animator.SetInteger("comboStep", currentComboStep);          

        //Debug.Log(checkAttack());
    }
    void performComboAttack()
    {
        isAttacking = true;

        animator.SetInteger("comboStep", currentComboStep);
        //ResetAllAttackParameters();
        //switch (currentComboStep)
        //{
        //    case 1:
        //        animator.SetBool("isAttack1", true);
        //        break;
        //    case 2:
        //        animator.SetBool("isAttack2", true);
        //        break;
        //    case 3:
        //        animator.SetBool("isAttack3", true);
        //        break;
        //    default:
        //        animator.SetBool("isAttack1", true);
        //        break;
        //}


        //StartCoroutine(ComboAttackCooldown(attackSpeed));
    }


    public void OnAttackFinished()
    {
        isAttacking = false;
        // Tăng currentComboStep để chuyển sang đòn tiếp theo trong combo, hoặc reset về 1 nếu đã đủ 3 đòn
        if (currentComboStep < 3)
            currentComboStep++;
        else
            currentComboStep = 1;

        Debug.Log("Attack finished. New combo step: " + currentComboStep);
    }


    //IEnumerator ComboAttackCooldown(float cooldownTime)
    //{
    //    yield return new WaitForSeconds(cooldownTime);
    //    isAttacking = false;
    //    ResetAllAttackParameters();

       
    //    if (currentComboStep < 3)
    //        currentComboStep++;
    //    else
    //        currentComboStep = 1;
    //}

    //void ResetAllAttackParameters()
    //{
    //     animator.SetBool("isAttack1", false);
    //     animator.SetBool("isAttack2", false);
    //     animator.SetBool("isAttack3", false);
    //}

}
