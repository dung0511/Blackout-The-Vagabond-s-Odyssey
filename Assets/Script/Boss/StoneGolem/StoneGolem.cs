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
        public int maxHealth;
        

        public bool isAttacking = false;
        public bool isDead = false;
        public bool isHealed = false;
        public bool isImmune = false;

        public float attackSpeed = 1f;
        //private float lastAttackTime = 0f;

        private ProjecttileManager projectTileManager;
        private void Awake()
        {

            movementToPositionEvent = GetComponent<MovementToPositionEvent>();
            idleEvent = GetComponent<IdleEvent>();

        }
        void Start()
        {
            //Laser laser = GetComponent<Laser>();
          //  laser.enabled = false;
            maxHealth = health;
            animator = GetComponent<Animator>();
            projectTileManager = GetComponent<ProjecttileManager>();
        }

        void Update()
        {

            if (!isAttacking && checkAttack() && !getFiring())
            {
                performMeleeAttack();
            }
            if (!isAttacking && !checkAttack() && !getFiring())
            {
                performRangedAttack();
            }
            if ((health <= (maxHealth * 0.5f)) && !getFiring() && !isHealed)
            {
                animator.SetBool("isImmune", true);
                isHealed=true;
                StartCoroutine(ImmuneCooldown());
            }
        }
        void performRangedAttack()
        {
            isAttacking = true;
            int attackType = Random.Range(1, 3);
            ResetRangedAttackParameters();

            switch (attackType)
            {
                case 1:
                    animator.SetBool("isRangedAttack", true);
                    break;
                case 2:
                    animator.SetBool("isLaserCast", true);
                    break;

            }
            StartCoroutine(RangedAttackCooldown(attackSpeed));
        }

        void performMeleeAttack()
        {
            isAttacking = true;

            ResetMeleeAttackParameters();
            animator.SetBool("isMeleeAtatck", true);

            StartCoroutine(MeleeAttackCooldown());
        }

        

        void ResetRangedAttackParameters()
        {
            animator.SetBool("isRangedAttack", false);
            animator.SetBool("isLaserCast", false);
        }

        void ResetMeleeAttackParameters()
        {
            animator.SetBool("isMeleeAtatck", false);
        }
        IEnumerator RangedAttackCooldown(float cooldownTime)
        {
            yield return new WaitForSeconds(cooldownTime);
            isAttacking = false;
            ResetRangedAttackParameters();
        }

        IEnumerator MeleeAttackCooldown()
        {
            yield return new WaitForSeconds(1f);
            isAttacking = false;
            ResetMeleeAttackParameters();
        }

        IEnumerator ImmuneCooldown()
        {
            yield return new WaitForSeconds(6f);
            
            animator.SetBool("isImmune", false);
        }

        private bool checkAttack()
        {
            return GetComponentInChildren<EnemyInteractZone>().isTouchPlayer;
        }

        private bool getFiring()
        {
            return GetComponent<ProjecttileManager>().isFiring;
        }

        //private bool getLasering()
        //{
        //    return GetComponentInChildren<Laser>().isLasering;
        //}
    }
}