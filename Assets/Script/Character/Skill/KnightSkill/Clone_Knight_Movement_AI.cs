using Pathfinding;
using System.Collections;
using UnityEngine;

public class Clone_Knight_Movement_AI : MonoBehaviour
{
    [SerializeField] private bool defaultFacingRight = true;
    [SerializeField] private PlayerDetailSO movementDetails;
    private CloneKnight clone;
    public Animator animator;

    [HideInInspector] public Transform target;
    [HideInInspector] public float nextWaypointDistance = 0.5f;
    private int currentWaypointIndex = 0;

    public float stopChaseDistance = 1f;
    public float resumeChaseDistance = 2f;
    public float resumeChaseDelay = 1f;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
   // private MovementToPosition movementToPosition;

    [HideInInspector] public float moveSpeed;
    private bool chasePlayer = false;
    private bool isWaitingToResumeChase = false;

    void Start()
    {
        animator=GetComponentInChildren<Animator>();
       // movementToPosition = GetComponent<MovementToPosition>();
        clone = GetComponent<CloneKnight>();
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = playerTransform;
        moveSpeed = movementDetails.playerSpeedAmount;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

    private void OnEnable()
    {
       
        if (isWaitingToResumeChase)
        {
            StartCoroutine(ResumeChaseAfterDelay());
        }
    }



    void UpdatePath()
    {
        if (chasePlayer && seeker.IsDone())
        {
            Vector3 adjustedTargetPosition = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z);
            seeker.StartPath(rb.position, adjustedTargetPosition, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypointIndex = 0;
        }
    }

    void Update()
    {
        //if (enemy.health > 0)
        //{
            CheckChaseCondition();

            MoveEnemy();
        //}
        //else
        //{
        //    rb.linearVelocity = Vector2.zero;
        //    path = null;
        //    chasePlayer = false;
        //}
    }

    void CheckChaseCondition()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        
        if (!chasePlayer && distanceToPlayer < clone.CloneKnightDetails.chaseDistance && !isWaitingToResumeChase)
        {
            chasePlayer = true;
            clone.movementToPositionEvent.CallMovementToPositionEvent(
                target.position,
                transform.position,
                moveSpeed,
                (target.position - transform.position).normalized
            );
        }

       
        if (chasePlayer && distanceToPlayer < stopChaseDistance)
        {
            rb.linearVelocity = Vector2.zero;
            chasePlayer = false;
            path = null;
            clone.idleEvent.CallIdleEvent();  
            StartCoroutine(ResumeChaseAfterDelay());
        }
    }

    private IEnumerator ResumeChaseAfterDelay()
    {
        isWaitingToResumeChase = true;
        yield return new WaitForSeconds(resumeChaseDelay);
       
        if (Vector3.Distance(transform.position, target.position) > resumeChaseDistance)
        {
            chasePlayer = true;
           
        }
        isWaitingToResumeChase = false;
    }
    
    private void MoveEnemy()
    {
        if (!chasePlayer || path == null || path.vectorPath.Count == 0) return;


        float distanceToWaypoint = Vector2.Distance(rb.position, (Vector2)path.vectorPath[currentWaypointIndex]);
        if (distanceToWaypoint < nextWaypointDistance)
        {
           animator.SetBool("isMoving", true);
            currentWaypointIndex++;


            if (currentWaypointIndex >= path.vectorPath.Count)
            {
                animator.SetBool("isMoving", false);
                currentWaypointIndex = 0;
                return;
            }
        }

        Vector2 targetPosition = (Vector2)path.vectorPath[currentWaypointIndex];
        Vector2 direction = (targetPosition - rb.position).normalized;


        clone.movementToPositionEvent.CallMovementToPositionEvent(
            targetPosition,
            transform.position,
            moveSpeed,
            direction
        );
       // UpdateEnemyFacingDirection();
    }
    public void StartChase()
    {
        chasePlayer = true;
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
}
