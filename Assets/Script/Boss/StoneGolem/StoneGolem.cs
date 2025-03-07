using System.Collections;
using UnityEngine;

namespace Assets.Script.Boss.StoneGolem
{
    public class StoneGolem : MonoBehaviour
    {
        public EnemyDetailSO enemyDetails;

        [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
        [HideInInspector] public IdleEvent idleEvent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Room roomBelong;

        public int damage;
        public int health;

        public bool isAttacking = false;
        private bool isDead = true;
        public bool isFiring;

        public float attackSpeed = 1f;
        private float lastAttackTime = 0f;

        private ProjecttileManager projectTileManager;
        private void Awake()
        {

            movementToPositionEvent = GetComponent<MovementToPositionEvent>();
            idleEvent = GetComponent<IdleEvent>();

        }
        void Start()
        {

            animator = GetComponent<Animator>();
            projectTileManager = GetComponent<ProjecttileManager>();
        }

        void Update()
        {
            //if (getFiring())
            //{
            //    animator.SetBool("isRangedAttack", true);
            //    projectTileManager.Fire();
            //}
            //else
            //{
            //    animator.SetBool("isRangedAttack", false);
            //}


            if (!isAttacking && checkAttack())
            {

                performMeleeAttack();
            }
            if (!isAttacking && !checkAttack())
            {
                performRangedAttack();
               
            }
            //else
            //{
            //    ResetAllAttackParameters();
            //}
            //isHurt = GetComponent<EnemyHealth>().isHurt;
            //if (!isAttacking && checkAttack())
            //{
            //    animator.SetBool("isMeleeAtatck", true);
            //    lastAttackTime = Time.time;
            //}
            //else if(!isAttacking && Time.time - lastAttackTime >= 2)
            //{
            //   
            //    
            //    lastAttackTime = Time.time;
            //    isAttacking = true;
            //}
            //ResetAllAttackParameters();
        }
        void performRangedAttack()
        {
            isAttacking = true;
            int attackType = Random.Range(1, 3);
            ResetAllAttackParameters();

            switch (attackType)
            {
                case 1:
                    animator.SetBool("isRangedAttack", true);
                     projectTileManager.Fire();
                    break;
                case 2:
                    animator.SetBool("isLaserCast", true);
                    //gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    break;

            }
            StartCoroutine(AttackCooldown(attackSpeed));
        }

        void ResetAllAttackParameters()
        {
            animator.SetBool("isMeleeAtatck", false);
            animator.SetBool("isRangedAttack", false);
            animator.SetBool("isLaserCast", false);

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


        void performMeleeAttack()
        {
            isAttacking = true;
            ResetAllAttackParameters();
            animator.SetBool("isMeleeAtatck", true);
            StartCoroutine(AttackCooldown(attackSpeed));
        }

        private bool getFiring()
        {
            return GetComponent<ProjecttileManager>().firingDone;
        }
    }
}