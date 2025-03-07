using System.Collections;
using UnityEngine;

namespace Assets.Script.Boss.StoneGolem
{
    public class StoneGolem : MonoBehaviour
    {

        public EnemyDetailSO enemyDetails;


        [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
        [HideInInspector] public IdleEvent idleEvent;
        public int damage;
        public int health;
        [HideInInspector] public Animator animator;
        public bool isHurt;
        public bool has3Attack;
        
        private bool isAttacking = false;

        public float attackSpeed = 1f;
        [HideInInspector] public Room roomBelong;
        private void Awake()
        {

            movementToPositionEvent = GetComponent<MovementToPositionEvent>();
            idleEvent = GetComponent<IdleEvent>();

        }
        void Start()
        {
            animator = GetComponent<Animator>();
            isHurt = false;
        }

        void Update()
        {
            
            if (!isAttacking && checkAttack()) performAttack();
            else ResetAllAttackParameters();
            isHurt = GetComponent<EnemyHealth>().isHurt;
            animator.SetBool("isHurt", isHurt);

        }
        void performAttack()
        {
            isAttacking = true;
            int attackType = has3Attack ? Random.Range(1, 4) : Random.Range(1, 3);
            ResetAllAttackParameters();

            switch (attackType)
            {
                case 1:
                    animator.SetBool("isAttack1", true);
                    break;
                case 2:
                    animator.SetBool("isAttack2", true);
                    break;
                case 3:
                    animator.SetBool("isAttack3", true);
                    break;
            }
            StartCoroutine(AttackCooldown(attackSpeed));
        }

        void ResetAllAttackParameters()
        {
            if (has3Attack)
            {
                animator.SetBool("isAttack1", false);
                animator.SetBool("isAttack2", false);
                animator.SetBool("isAttack3", false);
            }
            else
            {
                animator.SetBool("isAttack1", false);
                animator.SetBool("isAttack2", false);
            }
        }
        IEnumerator AttackCooldown(float cooldownTime)
        {
            yield return new WaitForSeconds(cooldownTime);
            isAttacking = false;
            ResetAllAttackParameters();
        }
        private bool checkAttack()
        {
            return GetComponentInChildren<EnemyInteractZone>().isTouchPlayer;
        }

    }
}