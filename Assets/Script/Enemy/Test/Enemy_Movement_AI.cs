using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System;

[DisallowMultipleComponent]
//public class Enemy_Movement_AI : MonoBehaviour
//{
//    [SerializeField] private MovementDetailsSO movementDetails;
//    private Enemy enemy;

//    [HideInInspector] public Transform target;

//    public float nextWaypointDistance = 3f;

//    Path path;
//    int currentWaypoint = 0;
//    bool reachedEndOfPath = false;
//    Seeker seeker;
//    Rigidbody2D rb;

//    [HideInInspector] public float moveSpeed;
//    private bool chasePlayer = false;
//    void Start()
//    {
//        enemy = GetComponent<Enemy>();
//        target = GameObject.FindGameObjectWithTag("Player").transform ;
//        moveSpeed = movementDetails.GetMoveSpeed();

//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();

//        InvokeRepeating("UpdatePath", 0f, 0.2f);

//    }
//    void UpdatePath()
//    {
//        if(seeker.IsDone()) seeker.StartPath(rb.position, target.position, OnPathComplete);
//    }
//    void OnPathComplete(Path p)
//    {
//        if (!p.error)
//        {
//            path = p;
//            currentWaypoint = 0;
//        }
//    }
//    void Update()
//    {
//        //if (path == null) return;
//        //if (currentWaypoint >= path.vectorPath.Count)
//        //{
//        //    reachedEndOfPath = true;
//        //    return;
//        //} else reachedEndOfPath = false;

//        //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized ;

//        //float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
//        //if (distance < nextWaypointDistance)
//        //{
//        //    currentWaypoint++;
//        //}
//        if (enemy.health > 0)
//        {
//            InvokeRepeating("chasePlayerCheck", 0f, 0.5f);
//            MoveEnemy();
//        }
//        //else if (enemy.health <= 0)
//        //{
//        //    enemy.health = 0;
//        //}

//    }

//    void chasePlayerCheck()
//    {
//        if (!chasePlayer && Vector3.Distance(transform.position, target.position) < enemy.enemyDetails.chaseDistance)
//        {
//            chasePlayer = true;
//        }
//        if (Vector3.Distance(transform.position, target.position) < .5f) chasePlayer = false;
//    }
//    private void MoveEnemy()
//    {
//        //if (!chasePlayer && Vector3.Distance(transform.position, target.position ) < enemy.enemyDetails.chaseDistance)
//        //{
//        //    chasePlayer = true;
//        //}
//        //if(Vector3.Distance(transform.position, target.position) < .5f) chasePlayer = false;
//        if (!chasePlayer) return;
//        else enemy.movementToPositionEvent.CallMovementToPositionEvent(target.position, transform.position, moveSpeed, (target.position - transform.position).normalized);
//    }

//    #region Validation

//#if UNITY_EDITOR

//    private void OnValidate()
//    {
//        Utility.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
//    }

//#endif

//    #endregion Validation
//}
public class Enemy_Movement_AI : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;
    private Enemy enemy;

    [HideInInspector] public Transform target;
    [HideInInspector] public float nextWaypointDistance = 0.3f;
    private int currentWaypointIndex = 0;

    public float stopChaseDistance = 1f; 
    public float resumeChaseDistance = 2f; 
    public float resumeChaseDelay = 1f; 

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;

    [HideInInspector] public float moveSpeed;
    private bool chasePlayer = false;
    private bool isWaitingToResumeChase = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = playerTransform;
        moveSpeed = movementDetails.GetMoveSpeed();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
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
        if (enemy.health > 0)
        {
            CheckChaseCondition();
            Debug.Log(chasePlayer);
            MoveEnemy();
        }
        else 
        {
            rb.linearVelocity = Vector2.zero;
            path = null;
            chasePlayer = false;
        }
    }

    void CheckChaseCondition()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

       
        if (!chasePlayer && distanceToPlayer < enemy.enemyDetails.chaseDistance
            && !isWaitingToResumeChase )
        {
            chasePlayer = true;
        }

        if (chasePlayer && distanceToPlayer < stopChaseDistance)
        {
            chasePlayer = false;
            enemy.idleEvent.CallIdleEvent();
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
            currentWaypointIndex++;

           
            if (currentWaypointIndex >= path.vectorPath.Count)
            {
                currentWaypointIndex = 0;
                return;
            }
        }

        Vector2 targetPosition = (Vector2)path.vectorPath[currentWaypointIndex];
        Vector2 direction = (targetPosition - rb.position).normalized;

        
        enemy.movementToPositionEvent.CallMovementToPositionEvent(
            targetPosition,
            transform.position,
            moveSpeed,
            direction
        );
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
}